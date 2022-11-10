using BoilerPlate.Core.Abstractions;

namespace BoilerPlate.Core.Identity.Commands.RefreshToken;

public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, Result<JsonWebToken>>
{
    private readonly IUserService _userService;
    private readonly IClock _clock;
    private readonly IDbContext _dbContext;
    private readonly AuthOptions _authOptions;
    private readonly IRequestStorage _requestStorage;
    private readonly IAuthManager _authManager;

    public RefreshTokenCommandHandler(IUserService userService, IClock clock, IDbContext dbContext,
        AuthOptions authOptions, IRequestStorage requestStorage, IAuthManager authManager)
    {
        _userService = userService;
        _clock = clock;
        _dbContext = dbContext;
        _authOptions = authOptions;
        _requestStorage = requestStorage;
        _authManager = authManager;
    }

    public async ValueTask<Result<JsonWebToken>> Handle(RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var userToken = await _dbContext.Set<UserToken>()
            .Include(e => e.User)
            .Where(e => e.ClientId == request.ClientId && e.RefreshToken == request.RefreshToken)
            .FirstOrDefaultAsync(cancellationToken);

        if (userToken is null || userToken.IsUsed || userToken.ExpiryAt < _clock.CurrentDate())
            return Result.Failure<JsonWebToken>(Error.Create("ExRT001", "Invalid request."));

        var refreshToken = Guid.NewGuid().ToString("N");

        var newUserToken = new UserToken
        {
            UserId = userToken.UserId,
            ClientId = request.ClientId,
            RefreshToken = refreshToken,
            ExpiryAt = _clock.CurrentDate().Add(_authOptions.RefreshTokenExpiry),
            DeviceType = userToken.DeviceType
        };
        _dbContext.Set<UserToken>().Add(newUserToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var user = (await _userService.GetUserByUsernameFullAsync(userToken.User!.Username, cancellationToken))!;

        _requestStorage.Set($"{userToken.UserId}{request.ClientId}",
            new UserIdentifier
            {
                UserId = user.UserId, IdentifierId = refreshToken,
                LastChangePassword = user.LastPasswordChangeAt!.Value,
                TokenId = newUserToken.UserTokenId.ToString()
            }, _authOptions.Expiry);

        var claims = Extensions.GenerateCustomClaims(user, userToken.DeviceType);

        var jwt = _authManager.CreateToken(user.UserId, request.ClientId, refreshToken,
            newUserToken.UserTokenId.ToString(), role: null,
            audience: null,
            claims: claims);

        //jwt claims clear, for result only
        jwt.Claims.Clear();

        return Result.Create(jwt, null!);
    }
}