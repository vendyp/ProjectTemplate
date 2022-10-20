namespace BoilerPlate.Core.Identity.Commands.ChangeEmail;

public sealed class ChangeEmailCommand : ICommand<Result>
{
    private Guid? _userId;
    public string NewEmail { get; set; } = null!;

    public Guid? GetUserId()
    {
        if (!_userId.HasValue)
            throw new NullReferenceException();

        return _userId.Value;
    }

    public ChangeEmailCommand SetUserId(Guid userId)
    {
        _userId = userId;
        return this;
    }
}