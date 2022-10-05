using BoilerPlate.Shared.Abstraction.Entities;

namespace BoilerPlate.Domain.Entities;

public class RolePermission : BaseEntity
{
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }
    public string PermissionId { get; set; } = null!;
    public Permission? Permission { get; set; }
}