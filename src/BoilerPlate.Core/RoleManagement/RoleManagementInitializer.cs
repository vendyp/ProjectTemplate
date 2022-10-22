using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Core.RoleManagement;

public class RoleManagementInitializer : IInitializer
{
    private readonly IDbContext _dbContext;

    public RoleManagementInitializer(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var dbSet = _dbContext.Set<Module>();

        if (await dbSet.AnyAsync(e => e.Code == RoleManagementConstant.ModuleName, cancellationToken))
            return;

        var userManagementModule = new Module
        {
            Code = RoleManagementConstant.ModuleName,
            Name = "Role Management",
            AsParentOnly = true
        };

        userManagementModule.Permissions.Add(new Permission
        {
            Code = RoleManagementConstant.PermissionRead,
            Description = "Permission to read"
        });

        userManagementModule.Permissions.Add(new Permission
        {
            Code = RoleManagementConstant.PermissionWrite,
            Description = "Permission to read/write"
        });

        userManagementModule.Permissions.Add(new Permission
        {
            Code = RoleManagementConstant.PermissionDelete,
            Description = "Permission to delete"
        });

        dbSet.Add(userManagementModule);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}