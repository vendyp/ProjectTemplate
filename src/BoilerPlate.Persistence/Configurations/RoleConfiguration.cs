using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Persistence.Configurations;

public sealed class RoleConfiguration : BaseEntityConfiguration<Role>
{
    protected override void EntityConfiguration(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.RoleId);
        builder.Property(e => e.RoleId).ValueGeneratedNever();
        builder.Property(e => e.Code).HasMaxLength(256);
        builder.Property(e => e.Name).HasMaxLength(256);
        builder.Property(e => e.NormalizedName).HasMaxLength(256);
    }
}