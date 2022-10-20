using BoilerPlate.Shared.Abstraction.Encryption;

namespace BoilerPlate.Core.Identity.Commands.ChangeEmail;

public class ChangeEmailCommandHandler : ICommandHandler<ChangeEmailCommand, Result>
{
    private readonly IUserService _userService;
    private readonly IDbContext _dbContext;
    private readonly IRng _rng;

    public ChangeEmailCommandHandler(IUserService userService, IDbContext dbContext, IRng rng)
    {
        _userService = userService;
        _dbContext = dbContext;
        _rng = rng;
    }

    public async Task<Result> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(request.GetUserId()!.Value, cancellationToken);

        if (user!.Email == request.NewEmail)
            return Result.Failure(IdentityErrors.SameAsOldEmail);

        user.Email = request.NewEmail;
        user.EmailActivationAt = null;
        user.EmailActivationCode = _rng.Generate(512);
        user.EmailActivationStatus = EmailActivationStatus.NeedActivation;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}