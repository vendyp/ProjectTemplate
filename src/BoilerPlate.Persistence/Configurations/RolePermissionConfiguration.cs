using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Persistence.Configurations;

public sealed class RolePermissionConfiguration : BaseEntityConfiguration<RolePermission>
{
    protected override void EntityConfiguration(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(e => new { e.RoleId, e.PermissionId });
    }
}