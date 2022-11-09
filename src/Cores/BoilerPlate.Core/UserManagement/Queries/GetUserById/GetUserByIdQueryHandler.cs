using BoilerPlate.Core.Abstractions;
using BoilerPlate.Core.Contracts;

namespace BoilerPlate.Core.UserManagement.Queries.GetUserById;

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Maybe<UserDetailResponse>>
{
    private readonly IServiceProvider _serviceProvider;

    public GetUserByIdQueryHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async ValueTask<Maybe<UserDetailResponse>> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        var user = await userService.GetUserByUserIdFullAsync(request.UserId, cancellationToken);
        if (user is null)
            return Maybe<UserDetailResponse>.None;

        var response = new UserDetailResponse(user);

        return response;
    }
}