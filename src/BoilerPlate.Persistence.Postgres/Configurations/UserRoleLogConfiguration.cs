using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Persistence.Postgres.Configurations;

public sealed class UserRoleLogConfiguration : BaseEntityConfiguration<UserRoleLog>
{
    protected override void EntityConfiguration(EntityTypeBuilder<UserRoleLog> builder)
    {
        builder.HasKey(e => e.UserRoleLogId);
        builder.Property(e => e.UserRoleLogId).ValueGeneratedNever();
    }
}