namespace BoilerPlate.Core.RoleManagement.Commands.EditRole;

public sealed class EditModuleDtoValidator : AbstractValidator<EditModuleDto>
{
    public EditModuleDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(e => e.ModuleId).NotEmpty();

        When(e => e.Children.Count == 0, () => { RuleFor(e => e.GivenPermissions).NotEmpty(); });

        When(e => e.Children.Any(), () =>
        {
            RuleFor(e => e.GivenPermissions).Must(e => e.Count == 0);
            RuleForEach(e => e.Children).SetValidator(new EditModuleChildDtoValidator());
        });
    }
}

public sealed class EditModuleChildDtoValidator : AbstractValidator<EditModuleDto>
{
    public EditModuleChildDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(e => e.ModuleId).NotEmpty();
        RuleFor(e => e.GivenPermissions).NotEmpty();
        RuleFor(e => e.Children).Empty();
    }
}