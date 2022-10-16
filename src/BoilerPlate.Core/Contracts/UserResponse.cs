namespace BoilerPlate.Core.Contracts;

public class UserResponse
{
    public Guid? UserId { get; set; }
    public string? Username { get; set; }
    public string? FullName { get; set; }
    public UserState? UserState { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? CreatedAtServer { get; set; }
}