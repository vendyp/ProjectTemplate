namespace BoilerPlate.Core.RoleManagement;

public static class RoleManagementErrors
{
    public static Error RoleCodeAlreadyRegistered => new("RoleMgmt.Create", "Role code already registered.");
    public static Error RolePermissionNotFound => new("RoleMgmt.Create", "Permission not found.");
    public static Error RoleNotFound => new("RoleMgmt.Edit", "Permission not found.");
}