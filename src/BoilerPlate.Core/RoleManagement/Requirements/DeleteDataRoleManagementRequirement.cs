using BoilerPlate.Shared.Abstraction.Auth;

namespace BoilerPlate.Core.RoleManagement.Requirements;

public class DeleteDataRoleManagementRequirement : IRequirement
{
    public string Policy => RoleManagementConstant.PermissionDelete;
    public string Permission => RoleManagementConstant.PermissionDelete;
}