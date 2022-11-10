﻿using BoilerPlate.Core.Abstractions;
using BoilerPlate.Core.Contracts;

namespace BoilerPlate.Core.Identity.Queries.GetMe;

public sealed class GetMeQueryHandler : IQueryHandler<GetMeQuery, Maybe<MeResponse?>>
{
    private readonly IUserService _userService;

    public GetMeQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async ValueTask<Maybe<MeResponse?>> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
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