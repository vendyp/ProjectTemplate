namespace BoilerPlate.Shared.Abstraction.Entities;

public abstract class BaseEntity
{
    public string? CreatedBy { get; set; }
    public string? CreatedByName { get; set; }
    public DateTime? CreatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
    public string? LastUpdatedByName { get; set; }
    public DateTime? LastUpdatedAt { get; set; }

    public string? DeletedBy { get; set; }
    public string? DeletedByName { get; set; }
    public DateTime? DeletedByAt { get; set; }
}