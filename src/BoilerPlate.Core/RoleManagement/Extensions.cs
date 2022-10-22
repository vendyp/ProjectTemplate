using BoilerPlate.Core.RoleManagement.Requirements;
using BoilerPlate.Shared.Infrastructure.Auth;
using BoilerPlate.Shared.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Core.RoleManagement;

public static class Extensions
{
    public static void AddRoleManagement(this IServiceCollection services)
    {
        services.AddInitializer<RoleManagementInitializer>();
        services.AddRequirement<ReadDataRoleManagementRequirement>();
        services.AddRequirement<WriteDataRoleManagementRequirement>();
        services.AddRequirement<DeleteDataRoleManagementRequirement>();
    }
}