using BoilerPlate.Domain.Entities.Enums;
using BoilerPlate.Shared.Abstraction.Entities;

namespace BoilerPlate.Domain.Entities;

public class UserToken : BaseEntity
{
    public UserToken()
    {
        UserTokenId = Guid.NewGuid();
        IsUsed = false;
        DeviceType = DeviceType.Android;
    }

    /// <summary>
    /// Primary key of object
    /// </summary>
    public Guid UserTokenId { get; set; }

    /// <summary>
    /// Foreign key to user object
    /// </summary>
    public Guid UserId { get; set; }

    public User? User { get; set; }

    public string ClientId { get; set; } = default!;

    /// <summary>
    /// Key to use refresh token scenario
    /// </summary>
    public string RefreshToken { get; set; } = default!;

    /// <summary>
    /// Expiration of refresh token key
    /// </summary>
    public DateTime ExpiryAt { get; set; }

    /// <summary>
    /// Flag that will use to identify refresh token key is already used
    /// </summary>
    public bool IsUsed { get; set; }

    /// <summary>
    /// When that refresh token key successfully used
    /// </summary>
    public DateTime? UsedAt { get; set; }

    public DeviceType DeviceType { get; set; }

    public void UseUserToken(DateTime dt)
    {
        IsUsed = true;
        UsedAt = dt;
    }
}