using BoilerPlate.Core.Validators;

namespace BoilerPlate.Core.UserManagement.Commands.ChangePasswordUser;

public class ChangePasswordUserCommandValidator : AbstractValidator<ChangePasswordUserCommand>
{
    public ChangePasswordUserCommandValidator()
    {
        RuleFor(e => e.NewPassword).SetValidator(new PasswordValidator());
    }
}