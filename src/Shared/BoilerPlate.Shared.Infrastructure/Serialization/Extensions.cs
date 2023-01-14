using BoilerPlate.Shared.Abstraction.Serialization;
using BoilerPlate.Shared.Infrastructure.Serialization.Jil;
using BoilerPlate.Shared.Infrastructure.Serialization.SystemTextJson;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Shared.Infrastructure.Serialization;

public static class Extensions
{
    public static void AddDefaultJsonSerialization(this IServiceCollection services)
    {
        // this can be switch easily to another json serializer for example
        //services.AddSingleton<IJsonSerializer, JilJsonSerializer>();
        services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
    }
}