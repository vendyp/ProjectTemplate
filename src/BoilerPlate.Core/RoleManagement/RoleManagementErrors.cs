namespace BoilerPlate.Core.RoleManagement;

public static class RoleManagementErrors
{
    public static Error RoleCodeAlreadyRegistered => new("RoleMgmt.Create", "Role code already registered.");
    public static Error RolePermissionNotFound => new("RoleMgmt.Create", "Permission not found.");
    public static Error RoleNotFound => new("RoleMgmt.Edit", "Permission not found.");
    public static Error ModuleNotFound => new("RoleMgmt.Edit", "Module not found");
    public static Error PermissionNotMatched => new("RoleMgmt.Edit", "Permission does not match");
    public static Error InvalidRequest => new("RoleMgmt.Edit", "Invalid request");
    public static Error SubModuleNotFound => new("RoleMgmt.Edit", "Sub Module not found");
}