using BoilerPlate.Shared.Abstraction.Auth;
using BoilerPlate.Shared.Abstraction.Storage;
using BoilerPlate.Shared.Abstraction.Time;
using BoilerPlate.Shared.Infrastructure.Auth;

namespace BoilerPlate.Core.Identity.Commands.SignIn;

public sealed class SignInCommandHandler : ICommandHandler<SignInCommand, Result<JsonWebToken>>
{
    private readonly IUserService _userService;
    private readonly IDbContext _dbContext;
    private readonly IClock _clock;
    private readonly AuthOptions _authOptions;
    private readonly IAuthManager _authManager;
    private readonly IRequestStorage _requestStorage;
    private readonly IPermissionService _permissionService;

    public SignInCommandHandler(IUserService userService, IDbContext dbContext, IClock clock, AuthOptions authOptions,
        IAuthManager authManager, IRequestStorage requestStorage, IPermissionService permissionService)
    {
        _userService = userService;
        _dbContext = dbContext;
        _clock = clock;
        _authOptions = authOptions;
        _authManager = authManager;
        _requestStorage = requestStorage;
        _permissionService = permissionService;
    }

    public async Task<Result<JsonWebToken>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByUsernameFullAsync(request.Username, cancellationToken);
        if (user?.Password is null)
            return Result.Failure<JsonWebToken>(IdentityErrors.InvalidUsernameAndPassword);

        if (!_userService.VerifyPassword(user.Password!, request.Password))
            return Result.Failure<JsonWebToken>(IdentityErrors.InvalidUsernameAndPassword);

        var refreshToken = Guid.NewGuid().ToString("N");

        var newUserToken = new UserToken
        {
            UserId = user.UserId,
            ClientId = request.ClientId,
            RefreshToken = refreshToken,
            ExpiryAt = _clock.CurrentDate().Add(_authOptions.RefreshTokenExpiry),
            DeviceType = request.GetDeviceType()
        };

        _dbContext.Set<UserToken>().Add(newUserToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _requestStorage.Set($"{user.UserId}{request.ClientId}",
            new UserIdentifier
            {
                UserId = user.UserId, IdentifierId = refreshToken,
                LastChangePassword = user.LastPasswordChangeAt!.Value, TokenId = newUserToken.UserTokenId.ToString()
            }, _authOptions.Expiry);

        var claims = Extensions.GenerateCustomClaims(user, request.GetDeviceType(), await _permissionService.GetAllPermissionCodeAsync(cancellationToken));

        var jwt = _authManager.CreateToken(user.UserId, request.ClientId, refreshToken,
            newUserToken.UserTokenId.ToString(), role: null, audience: null,
            claims: claims);

        //jwt claims clear, for result only
        jwt.Claims.Clear();

        return Result.Create(jwt, null!);
    }
}