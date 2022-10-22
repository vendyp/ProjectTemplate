using BoilerPlate.Shared.Abstraction.Auth;

namespace BoilerPlate.Core.RoleManagement.Requirements;

public class WriteDataRoleManagementRequirement : IRequirement
{
    public string Policy => RoleManagementConstant.PermissionWrite;
    public string Permission => RoleManagementConstant.PermissionWrite;
}