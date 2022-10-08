using BoilerPlate.Shared.Abstraction.Commands;
using BoilerPlate.Shared.Abstraction.Primitives;

namespace BoilerPlate.Core.UserManagement.EditUser;

public class EditUserCommand : ICommand<Result>
{
    public Guid UserId { get; init; }
    public string? FullName { get; init; }
    public string? AboutMe { get; init; }
}