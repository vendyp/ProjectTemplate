namespace BoilerPlate.Shared.Abstraction.Auth;

public sealed class JsonWebToken
{
    public JsonWebToken()
    {
        Claims = new Dictionary<string, IEnumerable<string>>();
    }

    public Guid UserId { get; init; }
    public long TokenExpiry { get; init; }
    public string AccessToken { get; init; } = default!;
    public string RefreshToken { get; init; } = default!;
    public string Username { get; init; } = default!;
    public string Email { get; init; } = default!;
    public IDictionary<string, IEnumerable<string>> Claims { get; }
}