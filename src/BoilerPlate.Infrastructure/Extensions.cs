using BoilerPlate.Core.Abstractions;
using BoilerPlate.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Infrastructure;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}