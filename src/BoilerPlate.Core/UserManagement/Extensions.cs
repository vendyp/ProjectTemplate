using BoilerPlate.Core.UserManagement.Requirements;
using BoilerPlate.Shared.Infrastructure.Auth;
using BoilerPlate.Shared.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Core.UserManagement;

public static class Extensions
{
    public static void AddUserManagement(this IServiceCollection services)
    {
        services.AddInitializer<UserManagementInitializer>();
        services.AddRequirement<ReadDataUserManagementRequirement>();
        services.AddRequirement<WriteDataUserManagementRequirement>();
        services.AddRequirement<DeleteDataUserManagementRequirement>();
    }
}