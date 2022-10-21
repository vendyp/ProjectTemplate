using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Domain.Entities;

public class RoleModule : BaseEntity
{
    public RoleModule()
    {
        RoleModuleId = Guid.NewGuid();
        RoleModuleChildren = new HashSet<RoleModuleChildren>();
        RoleModuleGivenPermissions = new HashSet<RoleModuleGivenPermission>();
    }

    public Guid RoleModuleId { get; set; }

    public Guid RoleId { get; set; }
    public Guid ModuleId { get; set; }
    public Module? Module { get; set; }

    public ICollection<RoleModuleChildren> RoleModuleChildren { get; set; }
    public ICollection<RoleModuleGivenPermission> RoleModuleGivenPermissions { get; set; }
}

public class RoleModuleConfiguration : BaseEntityConfiguration<RoleModule>
{
    protected override void EntityConfiguration(EntityTypeBuilder<RoleModule> builder)
    {
        builder.HasKey(e => e.RoleModuleId);
        builder.Property(e => e.RoleModuleId).ValueGeneratedNever();

        builder.HasOne(e => e.Module)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);
    }
}