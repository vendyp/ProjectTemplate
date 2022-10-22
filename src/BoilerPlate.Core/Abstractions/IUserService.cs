namespace BoilerPlate.Core.Abstractions;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);

    /// <summary>
    /// Get user by username, full meaning to gets deep into role, role modules, its children and permissions
    /// </summary>
    /// <param name="username"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<User?> GetUserByUsernameFullAsync(string username, CancellationToken cancellationToken);

    bool VerifyPassword(string currentPassword, string password);
    string HashPassword(string password);
}