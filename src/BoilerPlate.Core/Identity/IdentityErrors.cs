namespace BoilerPlate.Core.Identity;

public class IdentityErrors
{
    public static Error InvalidUsernameAndPassword => new("Identity.SignIn", "Invalid username or password.");
}