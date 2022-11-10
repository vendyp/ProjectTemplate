using BoilerPlate.Core.Abstractions;

namespace BoilerPlate.Core.Identity.Commands.ChangeEmail;

public class ChangeEmailCommandHandler : ICommandHandler<ChangeEmailCommand, Result>
{
    private readonly IServiceProvider _serviceProvider;

    public ChangeEmailCommandHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async ValueTask<Result> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var rng = scope.ServiceProvider.GetRequiredService<IRng>();
        var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();

        var user = await userService.GetUserByIdAsync(request.GetUserId()!.Value, cancellationToken);

        if (user!.Email == request.NewEmail)
            return Result.Failure(Error.Create("ExCE001", "Email same as before."));

        user.Email = request.NewEmail;
        user.EmailActivationAt = null;
        user.EmailActivationCode = rng.Generate(512);
        user.EmailActivationStatus = EmailActivationStatus.NeedActivation;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}