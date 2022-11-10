using System.Text.Json.Serialization;

namespace BoilerPlate.Shared.Api.Requests;

public record SignInRequest
{
    [JsonPropertyName("clientId")] public string ClientId { get; set; } = null!;

    [JsonPropertyName("username")] public string Username { get; set; } = null!;

    [JsonPropertyName("password")] public string Password { get; set; } = null!;
}