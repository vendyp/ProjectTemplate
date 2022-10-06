using FluentValidation;

namespace BoilerPlate.Core.UserManagement.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(e => e.Username).NotEmpty().MinimumLength(4).MaximumLength(100);
        RuleFor(e => e.Password).NotEmpty().MinimumLength(8).MaximumLength(64);
        RuleFor(e => e.Fullname).NotEmpty().MinimumLength(4).MaximumLength(100);
    }
}