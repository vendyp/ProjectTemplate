namespace BoilerPlate.Core.Contracts;

public sealed class UserDetailResponse : UserResponse
{
    public string? PhoneNumber { get; set; }

    public DateTime? BirthDate { get; set; }

    public UserGender? Gender { get; set; }

    public string? Email { get; set; }
}