using BoilerPlate.Domain.Entities;
using BoilerPlate.Infrastructure.Services;
using BoilerPlate.Persistence;
using BoilerPlate.Shared.Abstraction.Databases;
using BoilerPlate.UnitTests.Mocks;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace BoilerPlate.IntegrationTests.Services;

public class UserServiceTests
{
    private readonly IDbContext _dbContext;
    private readonly UserService _service;

    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<SqlServerDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new SqlServerDbContext(options,
            new ContextBuilder().Build().Object,
            new ClockBuilder().Build().Object);

        _service = new UserService(_dbContext);
    }

    [Fact]
    public async Task TestUserServiceWhenDataInsertedThenGetByUserIdShouldNotBeNull()
    {
        const string username = "test@test.com";

        var user = new User
        {
            Username = username,
            NormalizedUsername = username.ToUpperInvariant(),
            FullName = username
        };

        _dbContext.Insert(user);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var result = await _service.GetUserByIdAsync(user.UserId, CancellationToken.None);
        result.ShouldNotBeNull();
        result.Username.ShouldBe(user.Username);
        result.NormalizedUsername.ShouldBe(user.NormalizedUsername);
        result.FullName.ShouldBe(user.FullName);
    }

    [Fact]
    public async Task TestUserServiceWhenDataInsertedThenGetByAnotherUserIdShouldBeNull()
    {
        const string username = "test@test.com";

        var user = new User
        {
            Username = username,
            NormalizedUsername = username.ToUpperInvariant(),
            FullName = username
        };

        _dbContext.Insert(user);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var result = await _service.GetUserByIdAsync(Guid.NewGuid(), CancellationToken.None);
        result.ShouldBeNull();
    }
}