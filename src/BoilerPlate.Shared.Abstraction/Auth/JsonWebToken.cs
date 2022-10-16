using System.Text.Json.Serialization;

namespace BoilerPlate.Shared.Abstraction.Auth;

public sealed class JsonWebToken
{
    public JsonWebToken()
    {
        Claims = new Dictionary<string, IEnumerable<string>>();
    }

    public Guid UserId { get; init; }
    public long Expiry { get; init; }
    public string AccessToken { get; init; } = null!;
    public string RefreshToken { get; init; } = null!;
    [JsonIgnore] public IDictionary<string, IEnumerable<string>> Claims { get; }
}