using BoilerPlate.Domain.Entities;

namespace BoilerPlate.Core.Abstractions;

public interface IRoleService
{
    Task<Role?> GetRoleOfAdministratorAsync(CancellationToken cancellationToken);
    Task<Role?> GetRoleOfUserAsync(CancellationToken cancellationToken);
    Task<Role?> GetRoleByRoleIdAsync(Guid roleId, CancellationToken cancellationToken);
    Task<Role?> GetRoleByCodeAsync(string code, CancellationToken cancellationToken);
}