namespace BoilerPlate.Core.UserManagement;

public static class UserManagementErrors
{
    public static Error UserAlreadyRegistered => new("UsrMgmt.Create", "User already registered.");
    public static Error UserNotFound => new("UsrMgmt.Edit", "User not found.");
    public static Error UserNotFoundInChangePassword => new("UsrMgmt.ChangePassword", "User not found.");
}