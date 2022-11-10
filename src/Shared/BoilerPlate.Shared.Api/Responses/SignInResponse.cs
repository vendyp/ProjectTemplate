using System.Text.Json.Serialization;

namespace BoilerPlate.Shared.Api.Responses;

public record SignInResponse
{
    [JsonPropertyName("userId")] public string UserId { get; set; } = null!;

    [JsonPropertyName("expiry")] public long Expiry { get; set; }

    [JsonPropertyName("accessToken")] public string AccessToken { get; set; } = null!;

    [JsonPropertyName("refreshToken")] public string RefreshToken { get; set; } = null!;
}