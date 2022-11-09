using BoilerPlate.Core.Abstractions;
using BoilerPlate.Core.Contracts;

namespace BoilerPlate.Core.Identity.Queries.GetMe;

public sealed class GetMeQueryHandler : IQueryHandler<GetMeQuery, Maybe<MeResponse?>>
{
    private readonly IServiceProvider _serviceProvider;

    public GetMeQueryHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async ValueTask<Maybe<MeResponse?>> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        var user = await userService.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return Maybe<MeResponse?>.None;

        var vm = new MeResponse
        {
            UserId = user.UserId,
            Username = user.Username,
            FullName = user.FullName
        };

        return vm;
    }
}