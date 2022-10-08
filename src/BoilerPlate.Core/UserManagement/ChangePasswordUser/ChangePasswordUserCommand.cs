using BoilerPlate.Shared.Abstraction.Commands;
using BoilerPlate.Shared.Abstraction.Primitives;

namespace BoilerPlate.Core.UserManagement.ChangePasswordUser;

public class ChangePasswordUserCommand : ICommand<Result>
{
    public Guid UserId { get; set; }
    public string NewPassword { get; set; } = null!;
}