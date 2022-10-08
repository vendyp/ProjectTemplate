using BoilerPlate.Domain.Entities;

namespace BoilerPlate.Core.Abstractions;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
}