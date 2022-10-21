using BoilerPlate.Shared.Abstraction.Auth;

namespace BoilerPlate.Core.UserManagement.Requirements;

public class ReadDataUserManagementRequirement : IRequirement
{
    public string Policy => UserManagementConstant.PermissionRead;
    public string Permission => UserManagementConstant.PermissionRead;
}