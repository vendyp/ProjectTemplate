using BoilerPlate.Shared.Abstraction.Databases;
using BoilerPlate.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Persistence;

public static class Extensions
{
    public const string DefaultConfigName = "SqlServer";

    public static void AddSqlServer(this IServiceCollection services, string configName = DefaultConfigName)
    {
        var options = services.GetOptions<SqlServerOptions>(configName);
        if (string.IsNullOrWhiteSpace(options.ConnectionString))
            throw new Exception("Connection string is missing");

        services.AddDbContext<SqlServerDbContext>(x => x.UseSqlServer(options.ConnectionString));
        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<SqlServerDbContext>());
    }

    public static void AddSqlServer(this IServiceCollection services, Action<SqlServerDbContextOptionsBuilder>? action,
        string configName = DefaultConfigName)
    {
        var options = services.GetOptions<SqlServerOptions>(configName);

        if (string.IsNullOrWhiteSpace(options.ConnectionString))
            throw new Exception("Connection string is missing");

        services.AddDbContext<SqlServerDbContext>(x => x.UseSqlServer(options.ConnectionString, action));
        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<SqlServerDbContext>());
    }
}