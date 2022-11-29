using System.Net.Http.Headers;
using System.Reflection;
using Refit;

namespace BoilerPlate.App;

public static class IntegrationApiExtensions
{
    public static void AddIntegrationApi(this IServiceCollection services, string baseAddress)
    {
        var settings = new RefitSettings();
        var apis = new Type[] { typeof(IIdentityApi) };
        foreach (var api in apis)
        {
            services
                .AddRefitClient(api, settings)
                .ConfigureHttpClient((p, c) => c.BaseAddress = new Uri(baseAddress))
                .AddHttpMessageHandler<DefaultHttpHandler>();
        }

        services.AddScoped<DefaultHttpHandler>();
    }
}

public class DefaultHttpHandler : DelegatingHandler
{
    private static readonly string[] IgnoreEndpoints =
    {
        "/api/identity/refresh"
    };

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (IgnoreEndpoints.All(x => x != request.RequestUri!.AbsolutePath))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "token");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}