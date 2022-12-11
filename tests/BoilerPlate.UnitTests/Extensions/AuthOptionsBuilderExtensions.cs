using BoilerPlate.Shared.Infrastructure.Auth;
using Moq;

namespace BoilerPlate.UnitTests.Extensions;

public static class AuthOptionsBuilderExtensions
{
    public static AuthOptions Create()
    {
        return new AuthOptions
        {
            Expiry = TimeSpan.FromMinutes(30),
            RefreshTokenExpiry = TimeSpan.FromMinutes(30)
        };
    }
}