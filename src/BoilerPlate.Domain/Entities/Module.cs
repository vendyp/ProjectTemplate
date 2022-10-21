using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Domain.Entities;

public class Module : BaseEntity
{
    public Module()
    {
        ModuleId = Guid.NewGuid();
        SubModules = new HashSet<SubModule>();
        Permissions = new HashSet<Permission>();
    }

    public Guid ModuleId { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;

    /// <summary>
    /// If has sub module then as parent only default to false
    /// </summary>
    public bool AsParentOnly { get; set; }

    public ICollection<SubModule> SubModules { get; }
    public ICollection<Permission> Permissions { get; }
}

public class ModuleConfiguration : BaseEntityConfiguration<Module>
{
    protected override void EntityConfiguration(EntityTypeBuilder<Module> builder)
    {
        builder.HasKey(e => e.ModuleId);
        builder.Property(e => e.ModuleId).ValueGeneratedNever();

        builder.Property(e => e.Code).HasMaxLength(256);
        builder.HasIndex(e => e.Code).IsUnique();

        builder.Property(e => e.Name).HasMaxLength(256);
    }
}