using BoilerPlate.Domain.Entities.Enums;
using BoilerPlate.Shared.Abstraction.Entities;

namespace BoilerPlate.Domain.Entities;

public class UserRoleLog : BaseEntity
{
    public UserRoleLog()
    {
        UserRoleLogId = Guid.NewGuid();
        Type = UserRoleLogType.Given;
    }

    /// <summary>
    /// Primary key of object
    /// </summary>
    public Guid UserRoleLogId { get; set; }

    /// <summary>
    /// Foreign key to User object
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Foreign key to Role object
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// Default value is UserRoleLogType.Given
    /// </summary>
    public UserRoleLogType Type { get; set; }
}