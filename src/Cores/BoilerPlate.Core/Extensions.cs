using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using BoilerPlate.Core.Behaviours;
using Microsoft.AspNetCore.Identity;

[assembly: InternalsVisibleTo("BoilerPlate.FunctionalTests")]
[assembly: InternalsVisibleTo("BoilerPlate.IntegrationTests")]
[assembly: InternalsVisibleTo("BoilerPlate.UnitTests")]

namespace BoilerPlate.Core;

public static class Extensions
{
    public static Dictionary<string, IEnumerable<string>> GenerateCustomClaims(User user,
        DeviceType deviceType)
    {
        var claims = new Dictionary<string, IEnumerable<string>>
        {
            ["xid"] = new[] { user.UserId.ToString() },
            ["usr"] = new[] { user.Username },
            ["deviceType"] = new[] { deviceType.ToString() }
        };

        foreach (var userRole in user.UserRoles)
            claims.Add(ClaimTypes.Role, new[] { userRole.RoleId });

        return claims;
    }

    public static void AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Singleton);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
    }
}