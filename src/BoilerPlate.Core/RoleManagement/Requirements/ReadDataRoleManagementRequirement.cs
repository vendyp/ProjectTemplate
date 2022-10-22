using BoilerPlate.Shared.Abstraction.Auth;

namespace BoilerPlate.Core.RoleManagement.Requirements;

public class ReadDataRoleManagementRequirement : IRequirement
{
    public string Policy => RoleManagementConstant.PermissionRead;
    public string Permission => RoleManagementConstant.PermissionRead;
}