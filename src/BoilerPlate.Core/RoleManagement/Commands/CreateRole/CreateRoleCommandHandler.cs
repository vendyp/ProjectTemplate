namespace BoilerPlate.Core.RoleManagement.Commands.CreateRole;

public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, Result>
{
    private readonly IDbContext _dbContext;
    private readonly IRoleService _roleService;
    private readonly IPermissionService _permissionService;

    public CreateRoleCommandHandler(IDbContext dbContext, IRoleService roleService,
        IPermissionService permissionService)
    {
        _dbContext = dbContext;
        _roleService = roleService;
        _permissionService = permissionService;
    }

    public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var currentRole = await _roleService.GetRoleByCodeAsync(request.Code!, cancellationToken);
        if (currentRole is not null)
            return Result.Failure(ValidationErrors.RoleManagementErrors.RoleCodeAlreadyRegistered);

        var checkPermissions =
            await _permissionService.AllIdIsValidAsync(request.PermissionIds!.ToArray(), cancellationToken);
        if (checkPermissions)
            return Result.Failure(ValidationErrors.RoleManagementErrors.RoleCodeAlreadyRegistered);

        var role = new Role();

        foreach (var id in request.PermissionIds!)
            role.RolePermissions.Add(new RolePermission { PermissionId = id });

        role.Code = request.Code!;
        role.Name = request.Name!;
        role.NormalizedName = role.Name.ToUpperInvariant();

        _dbContext.Insert(role);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}