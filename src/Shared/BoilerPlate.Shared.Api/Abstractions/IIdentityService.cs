using BoilerPlate.Shared.Api.Requests;

namespace BoilerPlate.Shared.Api.Abstractions;

public interface IIdentityService
{
    [Post("/api/identity/sign-in")]
    Task<JsonWebToken> SignInAsync([Body] SignInRequest request);
}