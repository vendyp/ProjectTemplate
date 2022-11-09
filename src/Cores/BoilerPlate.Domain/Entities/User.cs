using BoilerPlate.Domain.Entities.Enums;
using BoilerPlate.Domain.ValueObjects;
using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

public sealed class UserConfiguration : BaseEntityConfiguration<User>
{
    protected override void EntityConfiguration(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.UserId);
        builder.Property(e => e.UserId).ValueGeneratedNever();
        builder.Property(e => e.Username).HasMaxLength(256);
        builder.Property(e => e.NormalizedUsername).HasMaxLength(256);
        builder.HasIndex(e => e.NormalizedUsername);
        builder.Property(e => e.Password).HasMaxLength(1024);
        builder.Property(e => e.FullName).HasMaxLength(256);
        builder.HasIndex(e => e.FullName);
        builder.Property(e => e.PhoneNumber).HasMaxLength(256);
        builder.Property(e => e.Country).HasMaxLength(256);
        builder.Property(e => e.AboutMe).HasMaxLength(256);
        builder.Property(e => e.EmailActivationCode).HasMaxLength(1024);
        builder.Property(x => x.Email).HasMaxLength(256)
            .HasConversion(x => x!.Value, x => new Email(x));
    }
}