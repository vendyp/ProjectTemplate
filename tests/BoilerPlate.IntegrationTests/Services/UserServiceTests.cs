using BoilerPlate.Domain.Entities;
using BoilerPlate.Infrastructure.Services;
using BoilerPlate.IntegrationTests.Fixtures;
using BoilerPlate.Shared.Abstraction.Databases;
using Shouldly;

namespace BoilerPlate.IntegrationTests.Services;

[Collection("SqlServerDatabase")]
public class UserServiceTests : IClassFixture<SqlServerDatabaseFixture>
{
    private readonly UserService _service;
    private readonly IDbContext _dbContext;

    public UserServiceTests(SqlServerDatabaseFixture fixture)
    {
        _dbContext = fixture.DbContext;
        _service = new UserService(fixture.DbContext);
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

    [Fact]
    public async Task TestUserServiceGetDataAdministratorShouldNotBeNull()
    {
        var result = await _service.GetUserByIdAsync(Guid.Empty, CancellationToken.None);
        result.ShouldNotBeNull();
    }
}