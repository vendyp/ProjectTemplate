using BoilerPlate.Shared.Abstraction.Entities;

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