using BoilerPlate.Core.Abstractions;
using BoilerPlate.Core.Responses;

namespace BoilerPlate.Core.UserManagement.Queries.GetUserById;

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Result<UserDetailResponse>>
{
    private readonly IUserService _userService;

    public GetUserByIdQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async ValueTask<Result<UserDetailResponse>> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByUserIdFullAsync(request.UserId, cancellationToken);
        if (user is null)
            return Result.Failure<UserDetailResponse>(Error.Create(404, "ExGUI001", "Data user not found."));

        var response = new UserDetailResponse(user);

        return response;
    }
}