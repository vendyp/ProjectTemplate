using BoilerPlate.Shared.Abstraction.Auth;

namespace BoilerPlate.Core.UserManagement.Requirements;

public class DeleteDataUserManagementRequirement : IRequirement
{
    public string Policy => UserManagementConstant.PermissionDelete;
    public string Permission => UserManagementConstant.PermissionDelete;
}