namespace BoilerPlate.Core.Identity.Commands.VerifyEmail;

public sealed class VerifyEmailCommandHandler : ICommandHandler<VerifyEmailCommand, Result>
{
    private readonly IServiceProvider _serviceProvider;

    public VerifyEmailCommandHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async ValueTask<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
        var clock = scope.ServiceProvider.GetRequiredService<IClock>();

        var user = await dbContext.Set<User>().Where(e => e.EmailActivationCode == request.Code)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null || user.EmailActivationStatus != EmailActivationStatus.NeedActivation)
            return Result.Failure(IdentityErrors.InvalidEmailActivationCode);

        user.EmailActivationCode = null;
        user.EmailActivationStatus = EmailActivationStatus.Activated;
        user.EmailActivationAt = clock.CurrentDate();

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}