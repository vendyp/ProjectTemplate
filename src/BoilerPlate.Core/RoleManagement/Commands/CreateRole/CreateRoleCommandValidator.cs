using BoilerPlate.Core.Validators;
using FluentValidation;

namespace BoilerPlate.Core.RoleManagement.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(e => e.Code).NotNull().NotEmpty().MinimumLength(3).MaximumLength(5);
        RuleFor(e => e.Name).NotNull().NotEmpty().MinimumLength(4).MaximumLength(50);
        RuleFor(e => e.PermissionIds).NotNull().NotEmpty().SetValidator(new DuplicationValidator()!);
    }
}