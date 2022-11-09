namespace BoilerPlate.Core.Identity;

public class IdentityErrors
{
    public static Error InvalidUsernameAndPassword => new("Identity.SignIn", "Invalid username or password.");
    public static Error UserNotFound => new("Identity.ChangePassword", "User invalid");
    public static Error PasswordIsSame => new("Identity.ChangePassword", "Invalid Password");
    public static Error RefreshTokenInvalidRequest => new("Identity.RefreshToken", "Invalid request");
    public static Error SameAsOldEmail => new("Identity.ChangeEmail", "New email same as old email");
    public static Error InvalidEmailActivationCode => new("Identity.VerifyEmail", "Expired or code is invalid");
}