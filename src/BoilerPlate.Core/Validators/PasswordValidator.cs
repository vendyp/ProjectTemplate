using FluentValidation;

namespace BoilerPlate.Core.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(e => e).NotEmpty().MinimumLength(8).MaximumLength(64);
    }
}