using BoilerPlate.Shared.Abstraction.Auth;
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

    public SignInCommandHandler(IUserService userService, IDbContext dbContext, IClock clock, AuthOptions authOptions,
        IAuthManager authManager)
    {
        _userService = userService;
        _dbContext = dbContext;
        _clock = clock;
        _authOptions = authOptions;
        _authManager = authManager;
    }

    public async Task<Result<JsonWebToken>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByUsernameAsync(request.Username, cancellationToken);
        if (user?.Password is null)
            return Result.Failure<JsonWebToken>(IdentityErrors.InvalidUsernameAndPassword);

        if (!_userService.VerifyPassword(user.Password!, request.Password))
            return Result.Failure<JsonWebToken>(IdentityErrors.InvalidUsernameAndPassword);

        var refreshToken = Guid.NewGuid().ToString("N");

        _dbContext.Set<UserToken>().Add(new UserToken
        {
            UserId = user.UserId,
            RefreshToken = refreshToken,
            ExpiryAt = _clock.CurrentDate().Add(_authOptions.RefreshTokenExpiry),
            DeviceType = request.GetDeviceType()
        });

        var claims = Extensions.GenerateCustomClaims(user, request.GetDeviceType());

        var jwt = _authManager.CreateToken(user.UserId, role: null, audience: null, claims: claims);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Create(jwt, null!);
    }
}