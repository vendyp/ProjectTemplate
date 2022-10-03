namespace BoilerPlate.Shared.Abstraction.Auth;

public interface IAuthManager
{
    JsonWebToken CreateToken(Guid userId, string? role, string? audience,
        IDictionary<string, IEnumerable<string>>? claims);
}