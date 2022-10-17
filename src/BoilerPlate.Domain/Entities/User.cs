using BoilerPlate.Domain.Entities.Enums;
using BoilerPlate.Domain.ValueObjects;
using BoilerPlate.Shared.Abstraction.Entities;

namespace BoilerPlate.Domain.Entities;

public class User : BaseEntity
{
    public User()
    {
        UserId = Guid.NewGuid();
        UserState = UserState.Active;
        EmailActivationStatus = EmailActivationStatus.Skip;

        UserRoles = new HashSet<UserRole>();
        UserTokens = new HashSet<UserToken>();
    }

    /// <summary>
    /// Primary key of the object
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Username of user, most likely it is email
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Store uppercase value of Username
    /// </summary>
    public string NormalizedUsername { get; set; } = null!;

    /// <summary>
    /// Password hashed with IPasswordHasher
    /// </summary>
    public string? Password { get; set; }

    public DateTime? LastPasswordChangeAt { get; set; }

    /// <summary>
    /// Value object of Email
    /// </summary>
    public Email? Email { get; set; }

    /// <summary>
    /// Full name of user
    /// </summary>
    public string FullName { get; set; } = null!;

    /// <summary>
    /// Default value is UserState.Active
    /// </summary>
    public UserState UserState { get; set; }

    /// <summary>
    /// Input using national number, without 0 or + or +_country_code
    /// </summary>
    public string? PhoneNumber { get; set; }

    public DateTime? BirthDate { get; set; }

    public UserGender? Gender { get; set; }

    public string? Country { get; set; }

    public string? AboutMe { get; set; }

    public int? CountryCode { get; set; }

    public EmailActivationStatus EmailActivationStatus { get; set; }

    public string? EmailActivationCode { get; set; }

    public DateTime? EmailActivationAt { get; set; }

    public ICollection<UserRole> UserRoles { get; }
    public ICollection<UserToken> UserTokens { get; }
}