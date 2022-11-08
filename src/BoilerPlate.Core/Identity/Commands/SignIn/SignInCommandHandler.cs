using BoilerPlate.Core.Abstractions;

namespace BoilerPlate.Core.Identity.Commands.SignIn;

public sealed class SignInCommandHandler : ICommandHandler<SignInCommand, Result<JsonWebToken>>
{
    private readonly IServiceProvider _serviceProvider;

    public SignInCommandHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async ValueTask<Result<JsonWebToken>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var clock = scope.ServiceProvider.GetRequiredService<IClock>();
        var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
        var authOptions = scope.ServiceProvider.GetRequiredService<AuthOptions>();
        var requestStorage = scope.ServiceProvider.GetRequiredService<IRequestStorage>();
        var authManager = scope.ServiceProvider.GetRequiredService<IAuthManager>();

        var user = await userService.GetUserByUsernameFullAsync(request.Username, cancellationToken);
        if (user?.Password is null)
            return Result.Failure<JsonWebToken>(IdentityErrors.InvalidUsernameAndPassword);

        if (!userService.VerifyPassword(user.Password!, request.Password))
            return Result.Failure<JsonWebToken>(IdentityErrors.InvalidUsernameAndPassword);

        var refreshToken = Guid.NewGuid().ToString("N");

        var newUserToken = new UserToken
        {
            UserId = user.UserId,
            ClientId = request.ClientId,
            RefreshToken = refreshToken,
            ExpiryAt = clock.CurrentDate().Add(authOptions.RefreshTokenExpiry),
            DeviceType = request.GetDeviceType()
        };

        dbContext.Set<UserToken>().Add(newUserToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        requestStorage.Set($"{user.UserId}{request.ClientId}",
            new UserIdentifier
            {
                UserId = user.UserId, IdentifierId = refreshToken,
                LastChangePassword = user.LastPasswordChangeAt!.Value, TokenId = newUserToken.UserTokenId.ToString()
            }, authOptions.Expiry);

        var claims = Extensions.GenerateCustomClaims(user, request.GetDeviceType());

        var jwt = authManager.CreateToken(user.UserId, request.ClientId, refreshToken,
            newUserToken.UserTokenId.ToString(), role: null, audience: null,
            claims: claims);

        //jwt claims clear, for result only
        jwt.Claims.Clear();

        return Result.Create(jwt, null!);
    }
}