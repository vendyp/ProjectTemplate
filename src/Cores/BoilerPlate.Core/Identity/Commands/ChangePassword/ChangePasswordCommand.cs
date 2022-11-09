namespace BoilerPlate.Core.Identity.Commands.ChangePassword;

public sealed class ChangePasswordCommand : ICommand<Result>
{
    private Guid _userId;

    public void SetUserId(Guid userId)
    {
        _userId = userId;
    }

    public Guid GetUserId() => _userId;
    public string CurrentPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public bool ForceReLogin { get; set; }
}