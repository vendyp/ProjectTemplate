using BoilerPlate.Shared.Abstraction.Databases;
using BoilerPlate.Shared.Abstraction.Time;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Domain.Services;

public class DomainInitializer : IInitializer
{
    private readonly IDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly DateTime _now;

    public DomainInitializer(IDbContext dbContext, IPasswordHasher<User> passwordHasher, IClock clock)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _now = clock.CurrentDate();
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await AddUserAdminAsync(cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task AddUserAdminAsync(CancellationToken cancellationToken)
    {
        if (await _dbContext.Set<User>().AnyAsync(e => e.UserId == Guid.Empty, cancellationToken: cancellationToken))
            return;

        await _dbContext.Set<User>().AddAsync(new User
        {
            UserId = Guid.Empty,
            Username = "admin",
            NormalizedUsername = "admin".ToUpper(),
            Password = _passwordHasher.HashPassword(default!, "Qwerty@1234"),
            LastPasswordChangeAt = _now,
            FullName = "Administrator",
            UserState = UserState.Active,
            CreatedAt = _now,
            CreatedByName = "system"
        }, cancellationToken);

        await _dbContext.Set<UserRole>().AddAsync(new UserRole
        {
            UserId = Guid.Empty,
            RoleId = RoleConstant.Administrator
        }, cancellationToken);
    }
}