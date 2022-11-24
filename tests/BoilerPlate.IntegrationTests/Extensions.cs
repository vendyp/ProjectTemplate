using BoilerPlate.IntegrationTests.Dependencies;
using BoilerPlate.Shared.Abstraction.Time;
using BoilerPlate.Shared.Infrastructure.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.IntegrationTests;

internal static class Extensions
{
    public static void AddIntegrationTestingServices(this IServiceCollection services)
    {
        services.AddSingleton<IClock, Clock>();
        services.AddMemoryRequestStorage();
    }
}