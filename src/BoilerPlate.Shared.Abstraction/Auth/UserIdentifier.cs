namespace BoilerPlate.Shared.Abstraction.Auth;

public class UserIdentifier
{
    public Guid UserId { get; set; }
    public string IdentifierId { get; set; } = null!;
    public DateTime LastChangePassword { get; set; }
}