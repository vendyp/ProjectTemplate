using BoilerPlate.Shared.Abstraction.Entities;

namespace BoilerPlate.Domain.Entities;

public class Permission : BaseEntity
{
    public string Id { get; set; } = null!;
    public string Description { get; set; } = null!;
}