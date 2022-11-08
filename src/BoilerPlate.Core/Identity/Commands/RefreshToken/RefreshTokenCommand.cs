namespace BoilerPlate.Core.Identity.Commands.RefreshToken;

public sealed class RefreshTokenCommand : ICommand<Result<JsonWebToken>>
{
    public string ClientId { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}