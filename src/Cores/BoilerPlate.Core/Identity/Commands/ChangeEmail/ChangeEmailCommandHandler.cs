using BoilerPlate.Core.Abstractions;

namespace BoilerPlate.Core.Identity.Commands.ChangeEmail;

public class ChangeEmailCommandHandler : ICommandHandler<ChangeEmailCommand, Result>
{
    private readonly IUserService _userService;
    private readonly IRng _rng;
    private readonly IDbContext _dbContext;

    public ChangeEmailCommandHandler(IUserService userService, IRng rng, IDbContext dbContext)
    {
        _userService = userService;
        _rng = rng;
        _dbContext = dbContext;
    }

    public async ValueTask<Result> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(request.GetUserId()!.Value, cancellationToken);

        if (user is null)
            return Result.Failure(Error.Create("ExCE001", "User not found."));

        if (user!.Email == request.NewEmail)
            return Result.Failure(Error.Create("ExCE002", "Email same as before."));

        user.Email = request.NewEmail;
        user.EmailActivationAt = null;
        user.EmailActivationCode = _rng.Generate(512);
        user.EmailActivationStatus = EmailActivationStatus.NeedActivation;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}