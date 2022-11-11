namespace BoilerPlate.Core.Identity.Commands.VerifyEmail;

public sealed record VerifyEmailCommand : ICommand<Result>
{
    public string Code { get; set; } = null!;
}