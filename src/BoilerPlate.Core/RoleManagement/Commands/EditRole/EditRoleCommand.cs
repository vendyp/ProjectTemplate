namespace BoilerPlate.Core.RoleManagement.Commands.EditRole;

public class EditRoleCommand : ICommand<Result>
{
    public Guid RoleId { get; set; }
    public string? Name { get; set; }
    public List<EditModuleDto>? ModuleDto { get; set; }
}