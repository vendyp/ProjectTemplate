namespace BoilerPlate.Core.Abstractions;

public interface IPermissionService
{
    /// <summary>
    /// Get permission by id
    /// </summary>
    /// <param name="id">a string of value id</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete</param>
    /// <returns></returns>
    Task<Permission?> GetPermissionByIdAsync(string id, CancellationToken cancellationToken);

    /// <summary>
    /// Check all parameters of ids is valid
    /// </summary>
    /// <param name="ids">Make sure parameter that passed here doesnt have duplication</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete</param>
    /// <returns></returns>
    Task<bool> AllIdIsValidAsync(string[] ids, CancellationToken cancellationToken);
}