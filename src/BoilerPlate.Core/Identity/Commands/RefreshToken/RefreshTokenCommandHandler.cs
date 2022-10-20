using BoilerPlate.Shared.Abstraction.Auth;
using BoilerPlate.Shared.Abstraction.Storage;
using BoilerPlate.Shared.Abstraction.Time;
using BoilerPlate.Shared.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Core.Identity.Commands.RefreshToken;

public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, Result<JsonWebToken>>
{
    private readonly IDbContext _dbContext;
    private readonly IClock _clock;
    private readonly AuthOptions _authOptions;
    private readonly IRequestStorage _requestStorage;
    private readonly IAuthManager _authManager;

    public RefreshTokenCommandHandler(IDbContext dbContext, IClock clock, AuthOptions authOptions,
        IRequestStorage requestStorage, IAuthManager authManager)
    {
        _dbContext = dbContext;
        _clock = clock;
        _authOptions = authOptions;
        _requestStorage = requestStorage;
        _authManager = authManager;
    }

    public async Task<Result<JsonWebToken>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var userToken = await _dbContext.Set<UserToken>()
            .Include(e => e.User)
            .Where(e => e.ClientId == request.ClientId && e.RefreshToken == request.RefreshToken)
            .FirstOrDefaultAsync(cancellationToken);

        if (userToken is null || userToken.IsUsed || userToken.ExpiryAt < _clock.CurrentDate())
            return Result.Failure<JsonWebToken>(IdentityErrors.RefreshTokenInvalidRequest);

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

        _requestStorage.Set($"{userToken.UserId}{request.ClientId}",
            new UserIdentifier
            {
                UserId = userToken.User!.UserId, IdentifierId = refreshToken,
                LastChangePassword = userToken.User!.LastPasswordChangeAt!.Value,
                TokenId = newUserToken.UserTokenId.ToString()
            }, _authOptions.Expiry);

        var claims = Extensions.GenerateCustomClaims(userToken.User!, userToken.DeviceType);

        var jwt = _authManager.CreateToken(userToken.User.UserId, request.ClientId, refreshToken,
            newUserToken.UserTokenId.ToString(), role: null,
            audience: null,
            claims: claims);
        //jwt claims clear, for result only
        jwt.Claims.Clear();

        return Result.Create(jwt, null!);
    }
}