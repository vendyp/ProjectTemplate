using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Shared.Abstraction.Entities;

public abstract class BaseEntityConfiguration<TBaseEntity> : IEntityTypeConfiguration<TBaseEntity>
    where TBaseEntity : BaseEntity
{
    public void Configure(EntityTypeBuilder<TBaseEntity> builder)
    {
        builder.Property(e => e.CreatedBy).HasMaxLength(maxLength: 256);
        builder.Property(e => e.CreatedByName).HasMaxLength(maxLength: 256);

        builder.Property(e => e.LastUpdatedBy).HasMaxLength(maxLength: 256);
        builder.Property(e => e.LastUpdatedByName).HasMaxLength(maxLength: 256);

        builder.Property(e => e.DeletedBy).HasMaxLength(maxLength: 256);
        builder.Property(e => e.DeletedByName).HasMaxLength(maxLength: 256);

        EntityConfiguration(builder);
    }

    protected abstract void EntityConfiguration(EntityTypeBuilder<TBaseEntity> builder);
}