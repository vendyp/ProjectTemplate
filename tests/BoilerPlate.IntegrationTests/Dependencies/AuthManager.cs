using BoilerPlate.Shared.Abstraction.Auth;

namespace BoilerPlate.IntegrationTests.Dependencies;

internal class AuthManager : IAuthManager
{
    public JsonWebToken CreateToken(Guid userId, string clientId, string refreshToken, string idd, string? role,
        string? audience,
        IDictionary<string, IEnumerable<string>>? claims)
    {
        return new JsonWebToken();
    }
}