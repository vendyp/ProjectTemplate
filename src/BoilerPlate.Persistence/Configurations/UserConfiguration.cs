using BoilerPlate.Domain.Entities;
using BoilerPlate.Domain.ValueObjects;
using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Persistence.Configurations;

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