using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Databases;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Infrastructure.Services;

internal class UserService : IUserService
{
    private readonly IDbContext _dbContext;
    private readonly IPasswordHasher<User>? _user;

    public UserService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public UserService(IDbContext dbContext, IPasswordHasher<User> user) : this(dbContext)
    {
        _dbContext = dbContext;
        _user = user;
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
        => _user?.VerifyHashedPassword(default!, currentPassword, password) == PasswordVerificationResult.Success;
}