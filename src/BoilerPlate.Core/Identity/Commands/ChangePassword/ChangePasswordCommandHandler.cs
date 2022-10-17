using BoilerPlate.Shared.Abstraction.Storage;
using BoilerPlate.Shared.Abstraction.Time;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Core.Identity.Commands.ChangePassword;

internal sealed class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, Result>
{
    private readonly IUserService _userService;
    private readonly IDbContext _dbContext;
    private readonly IClock _clock;
    private readonly IRequestStorage _requestStorage;

    public ChangePasswordCommandHandler(IUserService userService, IDbContext dbContext, IClock clock,
        IRequestStorage requestStorage)
    {
        _userService = userService;
        _dbContext = dbContext;
        _clock = clock;
        _requestStorage = requestStorage;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(request.GetUserId(), cancellationToken);
        if (user is null || user.Password is null)
            return Result.Failure(IdentityErrors.UserNotFound);

        if (_userService.VerifyPassword(user.Password!, request.NewPassword))
            return Result.Failure(IdentityErrors.PasswordIsSame);

        user.Password = _userService.HashPassword(request.NewPassword);
        user.LastPasswordChangeAt = _clock.CurrentDate();

        var listUserIdentity = new List<string>();

        if (request.ForceReLogin)
        {
            var userTokens = await _dbContext.Set<UserToken>().Where(e => e.UserId == user.UserId && e.IsUsed == false)
                .ToListAsync(cancellationToken);
            foreach (var userToken in userTokens)
            {
                userToken.IsUsed = true;
                listUserIdentity.Add(userToken.RefreshToken);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        if (!listUserIdentity.Any()) return Result.Success();

        foreach (var item in listUserIdentity)
            _requestStorage.Remove($"{user.UserId}{item}");

        return Result.Success();
    }
}