using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Databases;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Infrastructure.Services;

internal class UserService : IUserService
{
    private readonly IDbContext _dbContext;
    private readonly IPasswordHasher<User>? _passwordHasher;

    public UserService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public UserService(IDbContext dbContext, IPasswordHasher<User> passwordHasher) : this(dbContext)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        => _dbContext.Set<User>().Where(e => e.UserId == userId).FirstOrDefaultAsync(cancellationToken);

    public Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        username = username.ToUpperInvariant();
        return _dbContext.Set<User>().Where(e => e.NormalizedUsername == username)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public bool VerifyPassword(string currentPassword, string password)
        => _passwordHasher?.VerifyHashedPassword(default!, currentPassword, password) ==
           PasswordVerificationResult.Success;

    public string HashPassword(string password)
        => _passwordHasher?.HashPassword(default!, password)!;
}