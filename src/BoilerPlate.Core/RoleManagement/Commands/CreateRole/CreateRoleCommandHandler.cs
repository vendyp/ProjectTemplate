namespace BoilerPlate.Core.RoleManagement.Commands.CreateRole;

public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, Result>
{
    private readonly IDbContext _dbContext;
    private readonly IRoleService _roleService;

    public CreateRoleCommandHandler(IDbContext dbContext, IRoleService roleService)
    {
        _dbContext = dbContext;
        _roleService = roleService;
    }

    public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var currentRole = await _roleService.GetRoleByCodeAsync(request.Code!, cancellationToken);
        if (currentRole is not null)
            return Result.Failure(RoleManagementErrors.RoleCodeAlreadyRegistered);

        var role = new Role
        {
            Code = request.Code!,
            Name = request.Name!
        };
        role.NormalizedName = role.Name.ToUpperInvariant();

        _dbContext.Insert(role);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}