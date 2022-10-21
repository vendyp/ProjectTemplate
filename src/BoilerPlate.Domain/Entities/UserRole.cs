using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Domain.Entities;

public class UserRole : BaseEntity
{
    /// <summary>
    /// Primary key of the object
    /// </summary>
    public Guid UserId { get; set; }

    public User? User { get; set; }

    /// <summary>
    /// Primary key of the object
    /// </summary>
    public Guid RoleId { get; set; }

    public Role? Role { get; set; }
}

public sealed class UserRoleConfiguration : BaseEntityConfiguration<UserRole>
{
    protected override void EntityConfiguration(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(e => new { e.UserId, e.RoleId });
    }
}