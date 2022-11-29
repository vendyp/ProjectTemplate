using Refit;

namespace BoilerPlate.App.Abstractions;

public interface IIdentityApi : IApi
{
    [Post("/api/identity/sign-in")]
    Task<IApiResponse<LoginResponse>> Login([Body] LoginRequest body);
}

public record LoginRequest(string ClientId, string UserName, string Password);
public record LoginResponse(Guid ClientId, string AccessToken, string RefreshToken, long Expiry, Guid UseId, string UserName, string Emails);