namespace BoilerPlate.Core.UserManagement;

public class UserManagementInitializer : IInitializer
{
    private readonly IDbContext _dbContext;

    public UserManagementInitializer(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var userManagementModule = new Module
        {
            Code = UserManagementConstant.ModuleName,
            Name = "User Management",
            AsParentOnly = true
        };

        userManagementModule.Permissions.Add(new Permission
        {
            Code = UserManagementConstant.PermissionRead,
            Description = "Permission to read"
        });

        userManagementModule.Permissions.Add(new Permission
        {
            Code = UserManagementConstant.PermissionReadWrite,
            Description = "Permission to read/write"
        });

        userManagementModule.Permissions.Add(new Permission
        {
            Code = UserManagementConstant.PermissionDelete,
            Description = "Permission to delete"
        });

        _dbContext.Set<Module>().Add(userManagementModule);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}