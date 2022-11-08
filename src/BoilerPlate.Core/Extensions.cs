using System.Reflection;
using BoilerPlate.Core.Behaviours;
using BoilerPlate.Core.UserManagement;
using Microsoft.AspNetCore.Identity;

namespace BoilerPlate.Core;

public static class Extensions
{
    public static Dictionary<string, IEnumerable<string>> GenerateCustomClaims(User user,
        DeviceType deviceType, List<string> getAllPermissionCodesAsync)
    {
        var claims = new Dictionary<string, IEnumerable<string>>
        {
            ["xid"] = new[] { user.UserId.ToString() },
            ["usr"] = new[] { user.Username },
            ["deviceType"] = new[] { deviceType.ToString() }
        };

        if (user.UserRoles.Any(e => e.Role!.Code == Role.DefaultRoleAdminCode))
            claims.Add("permissions", getAllPermissionCodesAsync);
        else
        {
            var list = new List<string>();
            foreach (var item in user.UserRoles)
            {
                foreach (var item2 in item.Role!.RoleModules)
                {
                    list.AddRange(item2.RoleModuleGivenPermissions.Select(item3 => item3.Permission!.Code));
                    list.AddRange(from item3 in item2.RoleModuleChildren
                        from item4 in item3.RoleModuleGivenPermissions
                        select item4.Permission!.Code);
                }
            }

            claims.Add("permissions", list);
        }

        return claims;
    }

    public static void AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddMediator();

        services.AddUserManagement();
    }
}