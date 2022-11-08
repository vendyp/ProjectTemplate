
using BoilerPlate.Core.Abstractions;

namespace BoilerPlate.Core.Identity.Commands.RefreshToken;

public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, Result<JsonWebToken>>
{
    private readonly IServiceProvider _serviceProvider;

    public RefreshTokenCommandHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async ValueTask<Result<JsonWebToken>> Handle(RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var clock = scope.ServiceProvider.GetRequiredService<IClock>();
        var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
        var authOptions = scope.ServiceProvider.GetRequiredService<AuthOptions>();
        var requestStorage = scope.ServiceProvider.GetRequiredService<IRequestStorage>();
        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
        var authManager = scope.ServiceProvider.GetRequiredService<IAuthManager>();

        var userToken = await dbContext.Set<UserToken>()
            .Include(e => e.User)
            .Where(e => e.ClientId == request.ClientId && e.RefreshToken == request.RefreshToken)
            .FirstOrDefaultAsync(cancellationToken);

        if (userToken is null || userToken.IsUsed || userToken.ExpiryAt < clock.CurrentDate())
            return Result.Failure<JsonWebToken>(IdentityErrors.RefreshTokenInvalidRequest);

        var refreshToken = Guid.NewGuid().ToString("N");

        var newUserToken = new UserToken
        {
            UserId = userToken.UserId,
            ClientId = request.ClientId,
            RefreshToken = refreshToken,
            ExpiryAt = clock.CurrentDate().Add(authOptions.RefreshTokenExpiry),
            DeviceType = userToken.DeviceType
        };
        dbContext.Set<UserToken>().Add(newUserToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var user = (await userService.GetUserByUsernameFullAsync(userToken.User!.Username, cancellationToken))!;

        requestStorage.Set($"{userToken.UserId}{request.ClientId}",
            new UserIdentifier
            {
                UserId = user.UserId, IdentifierId = refreshToken,
                LastChangePassword = user.LastPasswordChangeAt!.Value,
                TokenId = newUserToken.UserTokenId.ToString()
            }, authOptions.Expiry);

        var claims = Extensions.GenerateCustomClaims(user, userToken.DeviceType,
            await permissionService.GetAllPermissionCodeAsync(cancellationToken));

        var jwt = authManager.CreateToken(user.UserId, request.ClientId, refreshToken,
            newUserToken.UserTokenId.ToString(), role: null,
            audience: null,
            claims: claims);

        //jwt claims clear, for result only
        jwt.Claims.Clear();

        return Result.Create(jwt, null!);
    }
}