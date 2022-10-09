using BoilerPlate.Shared.Abstraction.Commands;
using BoilerPlate.Shared.Abstraction.Primitives;

namespace BoilerPlate.Core.RoleManagement.Commands.CreateRole;

public class CreateRoleCommand : ICommand<Result>
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public List<string>? PermissionIds { get; set; }
}