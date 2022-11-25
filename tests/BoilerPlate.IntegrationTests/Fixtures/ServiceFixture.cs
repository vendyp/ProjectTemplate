using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain.Entities;
using BoilerPlate.Domain.Services;
using BoilerPlate.Infrastructure.Services;
using BoilerPlate.IntegrationTests.Dependencies;
using BoilerPlate.Persistence;
using BoilerPlate.Shared.Abstraction.Auth;
using BoilerPlate.Shared.Abstraction.Databases;
using BoilerPlate.Shared.Abstraction.Time;
using BoilerPlate.Shared.Infrastructure.Storage;
using BoilerPlate.UnitTests.Mocks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.IntegrationTests.Fixtures;

public class ServiceFixture : IDisposable
{
    private readonly SqlServerDbContext _db;
    public ServiceProvider ServiceProvider { get; }

    public ServiceFixture()
    {
        var options = new DbContextOptionsBuilder<SqlServerDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _db = new SqlServerDbContext(options, new ContextBuilder().Build().Object,
            new ClockBuilder().Build().Object);

        var appInit =
            new DomainInitializer(_db, new PasswordHasher<User>(), new ClockBuilder().Build().Object);
        appInit.ExecuteAsync(CancellationToken.None).GetAwaiter().GetResult();

        var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
        services.AddDbContext<SqlServerDbContext>(e =>
            e.UseInMemoryDatabase(Guid.NewGuid().ToString()));
        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<SqlServerDbContext>());
        services.AddSingleton(_db);
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<IClock, Clock>();
        services.AddSingleton<IAuthManager, AuthManager>();
        services.AddMemoryRequestStorage();

        ServiceProvider = services.BuildServiceProvider();
    }

    public void Dispose()
    {
        ServiceProvider.Dispose();
        _db.Dispose();
    }
}