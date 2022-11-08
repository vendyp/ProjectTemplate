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
        var dbSet = _dbContext.Set<Module>();

        if (await dbSet.AnyAsync(e => e.Code == UserManagementConstant.ModuleName, cancellationToken))
            return;

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
            Code = UserManagementConstant.PermissionWrite,
            Description = "Permission to read/write"
        });

        userManagementModule.Permissions.Add(new Permission
        {
            Code = UserManagementConstant.PermissionDelete,
            Description = "Permission to delete"
        });

        dbSet.Add(userManagementModule);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}