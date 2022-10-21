using BoilerPlate.Domain.Entities;
using BoilerPlate.Domain.Services;
using BoilerPlate.Persistence;
using BoilerPlate.Shared.Abstraction.Databases;
using BoilerPlate.UnitTests.Mocks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.IntegrationTests.Fixtures;

public class SqlServerDatabaseFixture : IDisposable
{
    public readonly IDbContext DbContext;
    private readonly SqlServerDbContext _db;

    public SqlServerDatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<SqlServerDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _db = new SqlServerDbContext(options, new ContextBuilder().Build().Object, new ClockBuilder().Build().Object);

        DbContext = _db;

        var appInit =
            new DomainInitializer(DbContext, new PasswordHasher<User>(), new ClockBuilder().Build().Object);
        appInit.ExecuteAsync(CancellationToken.None).GetAwaiter().GetResult();
    }

    public void Dispose() => _db.Dispose();
}