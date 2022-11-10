namespace BoilerPlate.Core.Identity.Commands.VerifyEmail;

public sealed class VerifyEmailCommandHandler : ICommandHandler<VerifyEmailCommand, Result>
{
    private readonly IDbContext _dbContext;
    private readonly IClock _clock;

    public VerifyEmailCommandHandler(IDbContext dbContext, IClock clock)
    {
        _dbContext = dbContext;
        _clock = clock;
    }

    public async ValueTask<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Set<User>().Where(e => e.EmailActivationCode == request.Code)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null || user.EmailActivationStatus != EmailActivationStatus.NeedActivation)
            return Result.Failure(Error.Create("ExVE001", "Data not found."));

        user.EmailActivationCode = null;
        user.EmailActivationStatus = EmailActivationStatus.Activated;
        user.EmailActivationAt = _clock.CurrentDate();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}