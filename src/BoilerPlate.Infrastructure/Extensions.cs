using System.Runtime.CompilerServices;
using BoilerPlate.Core;
using BoilerPlate.Core.Abstractions;
using BoilerPlate.Infrastructure.Services;
using BoilerPlate.Persistence;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("BoilerPlate.IntegrationTests")]

namespace BoilerPlate.Infrastructure;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddCore();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddSqlServer2();
    }
}