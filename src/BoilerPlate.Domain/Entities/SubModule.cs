using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Domain.Entities;

public class SubModule : BaseEntity
{
    public SubModule()
    {
        Permissions = new HashSet<Permission>();
    }

    public Guid SubModuleId { get; set; }
    public Guid ModuleId { get; set; }
    public Module? Module { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;

    public ICollection<Permission> Permissions { get; }
}

public class SubModuleConfiguration : BaseEntityConfiguration<SubModule>
{
    protected override void EntityConfiguration(EntityTypeBuilder<SubModule> builder)
    {
        builder.HasKey(e => e.SubModuleId);
        builder.Property(e => e.SubModuleId).ValueGeneratedNever();

        builder.Property(e => e.Code).HasMaxLength(256);
        builder.HasIndex(e => e.Code).IsUnique();

        builder.Property(e => e.Name).HasMaxLength(256);
    }
}