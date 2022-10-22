using BoilerPlate.Shared.Abstraction.Time;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Core.RoleManagement.Commands.EditRole;

public class EditRoleCommandHandler : ICommandHandler<EditRoleCommand, Result>
{
    private readonly IDbContext _dbContext;
    private readonly IRoleService _roleService;
    private readonly IModuleService _moduleService;
    private readonly IClock _clock;

    public EditRoleCommandHandler(IDbContext dbContext, IRoleService roleService, IModuleService moduleService,
        IClock clock)
    {
        _dbContext = dbContext;
        _roleService = roleService;
        _moduleService = moduleService;
        _clock = clock;
    }

    public async Task<Result> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleService.GetRoleByRoleIdAsync(request.RoleId, cancellationToken);
        if (role is null)
            return Result.Failure(RoleManagementErrors.RoleNotFound);

        if (request.Name.IsNotNullOrWhiteSpace())
            if (role.Name != request.Name!)
            {
                role.Name = request.Name!;
                role.NormalizedName = role.Name.ToUpperInvariant();
            }

        //validates input
        foreach (var item in request.ModuleDto!)
        {
            var module = await _moduleService.GetModuleByIdAsync(item.ModuleId, cancellationToken);
            if (module is null)
                return Result.Failure(RoleManagementErrors.ModuleNotFound);

            if (module.AsParentOnly && item.Children.Any())
                return Result.Failure(RoleManagementErrors.InvalidRequest);

            foreach (var item2 in item.GivenPermissions)
                if (!module.Permissions.Any(e => e.PermissionId == item2))
                    return Result.Failure(RoleManagementErrors.PermissionNotMatched);

            if (module.AsParentOnly) continue;

            foreach (var item2 in item.Children)
            {
                var subModule = module.SubModules.FirstOrDefault(e => e.SubModuleId == item2.ModuleId);
                if (subModule is null)
                    return Result.Failure(RoleManagementErrors.SubModuleNotFound);

                foreach (var item3 in item2.GivenPermissions)
                    if (!subModule.Permissions.Any(e => e.PermissionId == item3))
                        return Result.Failure(RoleManagementErrors.PermissionNotMatched);
            }
        }

        //update or delete existing
        foreach (var item in role.RoleModules)
            // update
            if (request.ModuleDto!.Any(e => e.ModuleId == item.ModuleId))
            {
                var dto = request.ModuleDto!.First(e => e.ModuleId == item.ModuleId);

                //delete permission if any
                foreach (var item2 in item.RoleModuleGivenPermissions)
                    if (!dto.GivenPermissions.Any(e => e == item2.PermissionId))
                        item2.DeletedByAt = _clock.CurrentDate();

                //insert permission if any
                foreach (var item2 in dto.GivenPermissions)
                    if (!item.RoleModuleGivenPermissions.Any(e => e.PermissionId == item2))
                        item.RoleModuleGivenPermissions.Add(new RoleModuleGivenPermission { PermissionId = item2 });

                //delete children if any
                foreach (var item2 in item.RoleModuleChildren)
                    if (!dto.Children.Any(e => e.ModuleId == item2.SubModuleId))
                    {
                        item2.DeletedByAt = _clock.CurrentDate();
                        foreach (var item3 in item2.RoleModuleGivenPermissions)
                            item3.DeletedByAt = _clock.CurrentDate();
                    }

                //insert children if any
                foreach (var item2 in dto.Children)
                    if (!item.RoleModuleChildren.Any(e => e.SubModuleId == item2.ModuleId))
                    {
                        var newRoleModuleChildren = new RoleModuleChildren { SubModuleId = item2.ModuleId };
                        foreach (var item3 in item2.GivenPermissions)
                            newRoleModuleChildren.RoleModuleGivenPermissions.Add(new RoleModuleGivenPermission
                                { PermissionId = item3 });

                        item.RoleModuleChildren.Add(newRoleModuleChildren);
                    }
            }
            // delete
            else
            {
                item.DeletedByAt = _clock.CurrentDate();
                foreach (var item2 in item.RoleModuleChildren)
                    item2.DeletedByAt = _clock.CurrentDate();

                foreach (var item2 in item.RoleModuleGivenPermissions)
                    item2.DeletedByAt = _clock.CurrentDate();
            }

        //insert if any
        foreach (var item in request.ModuleDto!)
            if (!role.RoleModules.Any(e => e.ModuleId == item.ModuleId))
            {
                var newRoleModule = new RoleModule
                {
                    ModuleId = item.ModuleId
                };

                foreach (var item2 in item.GivenPermissions)
                    newRoleModule.RoleModuleGivenPermissions.Add(new RoleModuleGivenPermission()
                        { PermissionId = item2 });

                foreach (var item2 in item.Children)
                {
                    var newSubModule = new RoleModuleChildren
                    {
                        SubModuleId = item2.ModuleId
                    };
                    foreach (var item3 in item2.GivenPermissions)
                        newSubModule.RoleModuleGivenPermissions.Add(new RoleModuleGivenPermission()
                            { PermissionId = item3 });

                    newRoleModule.RoleModuleChildren.Add(newSubModule);
                }

                role.RoleModules.Add(newRoleModule);
            }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public Task<Module?> GetModuleByIdAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.Set<Module>().Include(e => e.Permissions)
            .Include(e => e.SubModules)
            .Where(e => e.ModuleId == id)
            .FirstOrDefaultAsync(cancellationToken);
}