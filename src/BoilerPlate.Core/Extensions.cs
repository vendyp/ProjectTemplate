namespace BoilerPlate.Core;

public static class Extensions
{
    public static Dictionary<string, IEnumerable<string>> GenerateCustomClaims(User user,
        DeviceType deviceType)
    {
        var claims = new Dictionary<string, IEnumerable<string>>
        {
            ["xid"] = new[] { user.UserId.ToString() },
            ["givenName"] = new[] { user.Username },
            ["usr"] = new[] { user.Username },
            ["deviceType"] = new[] { deviceType.ToString() }
        };

        return claims;
    }
}