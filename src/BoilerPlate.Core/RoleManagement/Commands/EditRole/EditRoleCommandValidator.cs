namespace BoilerPlate.Core.RoleManagement.Commands.EditRole;

public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    public EditRoleCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(e => e.Name).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(e => e.ModuleDto).NotNull().NotEmpty().ForEach(e => e.SetValidator(new EditModuleDtoValidator()));
    }
}