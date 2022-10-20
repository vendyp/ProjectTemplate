using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Persistence.Postgres.Configurations;

public sealed class UserTokenConfiguration : BaseEntityConfiguration<UserToken>
{
    protected override void EntityConfiguration(EntityTypeBuilder<UserToken> builder)
    {
        builder.HasKey(e => e.UserTokenId);
        builder.Property(e => e.UserTokenId).ValueGeneratedNever();

        builder.Property(e => e.ClientId).HasMaxLength(256);

        builder.Property(e => e.RefreshToken).HasMaxLength(256);
    }
}