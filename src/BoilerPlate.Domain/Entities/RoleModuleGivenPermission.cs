using BoilerPlate.Shared.Abstraction.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlate.Domain.Entities;

public class RoleModuleGivenPermission : BaseEntity
{
    public Guid RoleModuleGivenPermissionId { get; set; }
    public Guid? RoleModuleId { get; set; }
    public RoleModule? RoleModule { get; set; }
    public Guid? RoleModuleChildrenId { get; set; }
    public RoleModuleChildren? RoleModuleChildren { get; set; }
    public Guid PermissionId { get; set; }
    public Permission? Permission { get; set; }
}

public class RoleModuleGivenPermissionConfiguration : BaseEntityConfiguration<RoleModuleGivenPermission>
{
    protected override void EntityConfiguration(EntityTypeBuilder<RoleModuleGivenPermission> builder)
    {
        builder.HasKey(e => e.RoleModuleGivenPermissionId);
        builder.Property(e => e.RoleModuleGivenPermissionId).ValueGeneratedNever();
    }
}