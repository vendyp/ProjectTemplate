using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Databases;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Infrastructure.Services;

public class RoleService : IRoleService
{
    private readonly DbSet<Role> _roles;

    public RoleService(IDbContext dbContext)
    {
        _roles = dbContext.Set<Role>();
    }

    public Task<Role?> GetRoleOfAdministratorAsync(CancellationToken cancellationToken)
    {
        var id = new Guid(Role.RoleAdminId);
        return _roles.Where(e => e.RoleId == id).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Role?> GetRoleOfUserAsync(CancellationToken cancellationToken)
    {
        var id = new Guid(Role.RoleUserId);
        return _roles.Where(e => e.RoleId == id).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Role?> GetRoleByRoleIdAsync(Guid roleId, CancellationToken cancellationToken)
        => _roles.Where(e => e.RoleId == roleId).FirstOrDefaultAsync(cancellationToken);

    public Task<Role?> GetRoleByCodeAsync(string code, CancellationToken cancellationToken)
        => _roles.Where(e => e.Code == code).FirstOrDefaultAsync(cancellationToken);
}