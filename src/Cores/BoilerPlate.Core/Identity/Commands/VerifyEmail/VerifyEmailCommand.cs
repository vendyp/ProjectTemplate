namespace BoilerPlate.Core.Identity.Commands.VerifyEmail;

public sealed class VerifyEmailCommand : ICommand<Result>
{
    public string Code { get; set; } = null!;
}