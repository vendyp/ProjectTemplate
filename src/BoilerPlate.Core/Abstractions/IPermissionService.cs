using BoilerPlate.Domain.Entities;

namespace BoilerPlate.Core.Abstractions;

public interface IPermissionService
{
    Task<Permission?> GetPermissionByIdAsync(string id, CancellationToken cancellationToken);
    Task<bool> AllIdIsValid(string[] ids, CancellationToken cancellationToken);
}