using BoilerPlate.Core.Abstractions;

namespace BoilerPlate.Core.Identity.Commands.ChangePassword;

internal sealed class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, Result>
{
    private readonly IServiceProvider _serviceProvider;

    public ChangePasswordCommandHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async ValueTask<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var clock = scope.ServiceProvider.GetRequiredService<IClock>();
        var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
        var requestStorage = scope.ServiceProvider.GetRequiredService<IRequestStorage>();

        var user = await userService.GetUserByIdAsync(request.GetUserId(), cancellationToken);
        if (user is null || user.Password is null)
            return Result.Failure(IdentityErrors.UserNotFound);

        if (userService.VerifyPassword(user.Password!, request.NewPassword))
            return Result.Failure(IdentityErrors.PasswordIsSame);

        user.Password = userService.HashPassword(request.NewPassword);
        user.LastPasswordChangeAt = clock.CurrentDate();

        var listUserIdentity = new List<string>();

        if (request.ForceReLogin)
        {
            var userTokens = await dbContext.Set<UserToken>().Where(e => e.UserId == user.UserId && e.IsUsed == false)
                .ToListAsync(cancellationToken);
            foreach (var userToken in userTokens)
            {
                userToken.IsUsed = true;
                listUserIdentity.Add(userToken.ClientId);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        if (!listUserIdentity.Any()) return Result.Success();

        foreach (var item in listUserIdentity)
            requestStorage.Remove($"{user.UserId}{item}");

        return Result.Success();
    }
}