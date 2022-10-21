using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Domain.Entities;

public class Permission : BaseEntity
{
    public Permission()
    {
        PermissionId = Guid.NewGuid();
    }

    public Guid PermissionId { get; set; }
    public Guid? ModuleId { get; set; }
    public Module? Module { get; set; }
    public Guid? SubModuleId { get; set; }
    public SubModule? SubModule { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
}

public sealed class PermissionConfiguration : BaseEntityConfiguration<Permission>
{
    protected override void EntityConfiguration(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(e => e.PermissionId);
        builder.Property(e => e.PermissionId).ValueGeneratedNever();

        builder.Property(e => e.Code).HasMaxLength(256);
        builder.HasIndex(e => e.Code).IsUnique();

        builder.Property(e => e.Description).HasMaxLength(1024);
    }
}