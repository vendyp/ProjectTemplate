using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Domain.Entities;

public class Role : BaseEntity
{
    public const string DefaultRoleAdminId = "00000000-0000-0000-0000-000000000000";
    public const string DefaultRoleUserAdminName = "admin";
    public const string DefaultRoleAdminCode = "adm";

    public const string DefaultRoleUserId = "655af068-a673-4bef-b69c-2938f2614fb4";
    public const string DefaultRoleUserName = "user";
    public const string DefaultRoleUserCode = "usr";

    public Role()
    {
        RoleId = Guid.NewGuid();
    }

    /// <summary>
    /// Primary key of an object
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// Abbreviation of it role full name
    /// </summary>
    public string Code { get; set; } = default!;

    /// <summary>
    /// Full name of it role
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Upper case of role name
    /// </summary>
    public string NormalizedName { get; set; } = default!;
}

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