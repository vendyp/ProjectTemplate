using BoilerPlate.Shared.Abstraction.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Shared.Infrastructure.Storage;

public static class Extensions
{
    public static void AddMemoryRequestStorage(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IRequestStorage, RequestStorage>();
    }
}