using BoilerPlate.Shared.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace BoilerPlate.Persistence.Postgres;

public static class Extensions
{
    public const string DefaultConfigName = "Postgres";

    public static void AddPostgres(this IServiceCollection services, string configName = DefaultConfigName)
    {
        var options = services.GetOptions<PostgresOptions>(configName);
        if (string.IsNullOrWhiteSpace(options.ConnectionString))
            throw new Exception("Connection string is missing");

        services.AddNpgsql<PostgresDbContext>(options.ConnectionString);
    }

    public static void AddPostgres(this IServiceCollection services, Action<NpgsqlDbContextOptionsBuilder>? action,
        string configName = DefaultConfigName)
    {
        var options = services.GetOptions<PostgresOptions>(configName);

        if (string.IsNullOrWhiteSpace(options.ConnectionString))
            throw new Exception("Connection string is missing");

        services.AddNpgsql<PostgresDbContext>(options.ConnectionString, action);
    }
}