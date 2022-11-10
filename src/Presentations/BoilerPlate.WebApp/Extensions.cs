using BoilerPlate.Shared.Infrastructure;
using BoilerPlate.WebApp.Models;

namespace BoilerPlate.WebApp;

internal static class Extensions
{
    public static void AddApplicationOptions(this IServiceCollection services, string configName = "app")
    {
        var options = services.GetOptions<ApplicationOptions>(configName);

        if (string.IsNullOrWhiteSpace(options.BaseUrl))
            throw new InvalidOperationException("Missing baseurl");
        
        services.AddSingleton(options);
    }
}