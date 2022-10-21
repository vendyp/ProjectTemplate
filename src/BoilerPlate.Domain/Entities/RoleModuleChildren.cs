using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Domain.Entities;

public class RoleModuleChildren : BaseEntity
{
    public RoleModuleChildren()
    {
        RoleModuleChildrenId = Guid.NewGuid();
        RoleModuleGivenPermissions = new HashSet<RoleModuleGivenPermission>();
    }

    public Guid RoleModuleChildrenId { get; set; }
    public Guid RoleModuleId { get; set; }
    public RoleModule? RoleModule { get; set; }
    public Guid SubModuleId { get; set; }
    public SubModule? SubModule { get; set; }

    public ICollection<RoleModuleGivenPermission> RoleModuleGivenPermissions { get; set; }
}

public class RoleModuleChildrenConfiguration : BaseEntityConfiguration<RoleModuleChildren>
{
    protected override void EntityConfiguration(EntityTypeBuilder<RoleModuleChildren> builder)
    {
        builder.HasKey(e => e.RoleModuleChildrenId);
        builder.Property(e => e.RoleModuleChildrenId).ValueGeneratedNever();

        builder.HasOne(e => e.SubModule)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);
    }
}