namespace BoilerPlate.Core.Abstractions;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
    bool VerifyPassword(string currentPassword, string password);
    string HashPassword(string password);
}