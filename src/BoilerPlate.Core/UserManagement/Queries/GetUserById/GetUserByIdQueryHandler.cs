namespace BoilerPlate.Core.UserManagement.Queries.GetUserById;

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Maybe<UserDetailResponse>>
{
    private readonly IUserService _userService;

    public GetUserByIdQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Maybe<UserDetailResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByUserIdFullAsync(request.UserId, cancellationToken);
        if (user is null)
            return Maybe<UserDetailResponse>.None;

        var response = new UserDetailResponse(user);

        return response;
    }
}