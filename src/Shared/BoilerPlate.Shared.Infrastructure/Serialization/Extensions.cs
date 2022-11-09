using BoilerPlate.Shared.Abstraction.Serialization;
using BoilerPlate.Shared.Infrastructure.Serialization.SystemTextJson;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Shared.Infrastructure.Serialization;

public static class Extensions
{
    public static void AddDefaultJsonSerialization(this IServiceCollection services)
    {
        services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
    }
}