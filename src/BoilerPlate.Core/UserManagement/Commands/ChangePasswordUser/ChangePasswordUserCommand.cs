namespace BoilerPlate.Core.UserManagement.Commands.ChangePasswordUser;

public class ChangePasswordUserCommand : ICommand<Result>
{
    public Guid UserId { get; set; }
    public string NewPassword { get; set; } = null!;
}