using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Databases;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Infrastructure.Services;

internal class UserService : IUserService
{
    private readonly IDbContext _dbContext;

    public UserService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        => _dbContext.Set<User>().Where(e => e.UserId == userId).FirstOrDefaultAsync(cancellationToken);

    public Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        username = username.ToUpperInvariant();
        return _dbContext.Set<User>().Where(e => e.NormalizedUsername == username)
            .FirstOrDefaultAsync(cancellationToken);
    }
}