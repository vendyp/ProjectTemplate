namespace BoilerPlate.Core.Identity;

public class IdentityErrors
{
    public static Error InvalidUsernameAndPassword => new("Identity.SignIn", "Invalid username or password.");
    public static Error UserNotFound => new("Identity.ChangePassword", "User invalid");
    public static Error PasswordIsSame => new("Identity.ChangePassword", "Invalid Password");
}