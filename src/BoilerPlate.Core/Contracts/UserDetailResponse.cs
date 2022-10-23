namespace BoilerPlate.Core.Contracts;

public sealed class UserDetailResponse : UserResponse
{
    public UserDetailResponse(User user) : base(user)
    {
        PhoneNumber = user.PhoneNumber;
        BirthDate = user.BirthDate;
        Email = user.Email;
        EmailActivationStatus = user.EmailActivationStatus;

        if (!user.UserRoles.Any()) return;

        Roles = new List<UserRoleResponse>();
        Roles.AddRange(user.UserRoles.Select(e => new UserRoleResponse
            { RoleId = e.RoleId, Name = e.Role?.Name ?? string.Empty }));
    }

    public string? PhoneNumber { get; set; }

    public DateTime? BirthDate { get; set; }

    public UserGender? Gender { get; set; }

    public string? Email { get; set; }
    public EmailActivationStatus? EmailActivationStatus { get; set; }

    /// <summary>
    /// This property will be only filled if called by user management
    /// </summary>
    public List<UserRoleResponse>? Roles { get; set; }
}