using BoilerPlate.Shared.Abstraction.Primitives;

namespace BoilerPlate.Core.UserManagement;

public static partial class ValidationErrors
{
    public static class UserManagementErrors
    {
        public static Error UserAlreadyRegistered => new("UsrMgmt.Create", "User already registered.");
        public static Error UserNotFound => new("UsrMgmt.Edit", "User not found.");
    }
}