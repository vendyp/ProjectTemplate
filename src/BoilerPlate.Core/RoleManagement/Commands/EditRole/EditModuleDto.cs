namespace BoilerPlate.Core.RoleManagement.Commands.EditRole;

public sealed class EditModuleDto
{
    public EditModuleDto()
    {
        Children = new List<EditModuleDto>();
        GivenPermissions = new List<Guid>();
    }

    public Guid ModuleId { get; set; }
    public List<Guid> GivenPermissions { get; set; }
    public List<EditModuleDto> Children { get; set; }
}