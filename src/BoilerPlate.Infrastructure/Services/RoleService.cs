using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Databases;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Infrastructure.Services;

internal class RoleService : IRoleService
{
    private readonly DbSet<Role> _roles;

    public RoleService(IDbContext dbContext)
    {
        _roles = dbContext.Set<Role>();
    }

    public IQueryable<Role> GetBaseQuery()
    {
        return _roles.Include(e => e.RoleModules)
            .ThenInclude(e => e.RoleModuleChildren)
            .ThenInclude(e => e.RoleModuleGivenPermissions)
            .Include(e => e.RoleModules)
            .ThenInclude(e => e.RoleModuleGivenPermissions)
            .AsQueryable();
    }

    public Task<Role?> GetRoleOfAdministratorAsync(CancellationToken cancellationToken)
    {
        var id = new Guid(Role.DefaultRoleAdminId);
        return GetBaseQuery().Where(e => e.RoleId == id).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Role?> GetRoleOfUserAsync(CancellationToken cancellationToken)
    {
        var id = new Guid(Role.DefaultRoleUserId);
        return GetBaseQuery().Where(e => e.RoleId == id).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Role?> GetRoleByRoleIdAsync(Guid roleId, CancellationToken cancellationToken)
        => GetBaseQuery().Where(e => e.RoleId == roleId).FirstOrDefaultAsync(cancellationToken);

    public Task<Role?> GetRoleByCodeAsync(string code, CancellationToken cancellationToken)
        => _roles.Where(e => e.Code == code).FirstOrDefaultAsync(cancellationToken);
}