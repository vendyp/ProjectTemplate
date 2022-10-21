namespace BoilerPlate.Core.RoleManagement.Commands.EditRole;

public class EditRoleCommandHandler : ICommandHandler<EditRoleCommand, Result>
{
    private readonly IDbContext _dbContext;
    private readonly IRoleService _roleService;

    public EditRoleCommandHandler(IDbContext dbContext, IRoleService roleService)
    {
        _dbContext = dbContext;
        _roleService = roleService;
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

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}