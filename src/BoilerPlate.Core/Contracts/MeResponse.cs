namespace BoilerPlate.Core.Contracts;

public class MeResponse
{
    public Guid? UserId { get; set; }
    public string? Username { get; set; }
    public string? FullName { get; set; }
    public string? ProfilePictureUrl { get; set; }
}