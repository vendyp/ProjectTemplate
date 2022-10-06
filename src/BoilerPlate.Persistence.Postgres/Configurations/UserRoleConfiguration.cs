using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Persistence.Postgres.Configurations;

public sealed class UserRoleConfiguration : BaseEntityConfiguration<UserRole>
{
    protected override void EntityConfiguration(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(e => new { e.UserId, e.RoleId });
    }
}