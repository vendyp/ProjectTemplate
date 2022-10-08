using BoilerPlate.Core.Validators;
using FluentValidation;

namespace BoilerPlate.Core.UserManagement.ChangePasswordUser;

public class ChangePasswordUserCommandValidator : AbstractValidator<ChangePasswordUserCommand>
{
    public ChangePasswordUserCommandValidator()
    {
        RuleFor(e => e.NewPassword).SetValidator(new PasswordValidator());
    }
}