namespace BoilerPlate.Core.Contracts;

public sealed class UserRoleResponse
{
    public Guid RoleId { get; set; }
    public string Name { get; set; } = null!;
}