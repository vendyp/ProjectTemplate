using BoilerPlate.Shared.Abstraction.Auth;

namespace BoilerPlate.Core.UserManagement.Requirements;

public class WriteDataUserManagementRequirement : IRequirement
{
    public string Policy => UserManagementConstant.PermissionWrite;
    public string Permission => UserManagementConstant.PermissionWrite;
}