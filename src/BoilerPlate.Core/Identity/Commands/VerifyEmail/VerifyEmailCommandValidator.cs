namespace BoilerPlate.Core.Identity.Commands.VerifyEmail;

public sealed class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
{
    public VerifyEmailCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(e => e.Code).NotEmpty();
    }
}