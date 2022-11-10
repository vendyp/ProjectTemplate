using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Shared.Api;

public static class Extensions
{
    public static void AddApi(this IServiceCollection services, string baseUrl)
    {
        var assemblies = typeof(Marker).Assembly.GetTypes();
        
    }
}