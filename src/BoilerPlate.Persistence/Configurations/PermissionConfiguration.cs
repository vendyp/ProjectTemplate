using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Persistence.Configurations;

public sealed class PermissionConfiguration : BaseEntityConfiguration<Permission>
{
    protected override void EntityConfiguration(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasMaxLength(256).ValueGeneratedNever();
        builder.Property(e => e.Description).HasMaxLength(1024);
    }
}