using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain.Entities;
using BoilerPlate.Domain.Services;
using BoilerPlate.Infrastructure.Services;
using BoilerPlate.IntegrationTests.Dependencies;
using BoilerPlate.Persistence;
using BoilerPlate.Shared.Abstraction.Auth;
using BoilerPlate.Shared.Abstraction.Databases;
using BoilerPlate.Shared.Abstraction.Encryption;
using BoilerPlate.Shared.Abstraction.Time;
using BoilerPlate.Shared.Infrastructure.Auth;
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
            new Clock());

        var appInit =
            new DomainInitializer(_db, new PasswordHasher<User>(), new Clock());
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
        services
            .AddSingleton<ISecurityProvider, SecurityProvider>()
            .AddSingleton<IEncryptor, Encryptor>()
            .AddSingleton<IHasher, Hasher>()
            .AddSingleton<IMd5, Md5>()
            .AddSingleton<IRng, Rng>();
        services.AddMemoryRequestStorage();
        services.AddSingleton(new AuthOptions
            { Expiry = new TimeSpan(7, 0, 0), RefreshTokenExpiry = new TimeSpan(7, 0, 0, 0) });

        ServiceProvider = services.BuildServiceProvider();
    }

    public void Dispose()
    {
        ServiceProvider.Dispose();
        _db.Dispose();
    }
}