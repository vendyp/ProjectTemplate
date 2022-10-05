using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Shared.Infrastructure.Services;

public static class Extensions
{
    public static void AddApplicationInitializer(this IServiceCollection services)
    {
        services.AddHostedService<ApplicationInitializer>();
    }
}