using System.Runtime.CompilerServices;
using BoilerPlate.Core;
using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain.Services;
using BoilerPlate.Infrastructure.Services;
using BoilerPlate.Persistence;
using BoilerPlate.Shared.Infrastructure.Services;
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
        services.AddInitializer<DomainInitializer>();
    }
}