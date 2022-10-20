using BoilerPlate.Core.Contracts;
using BoilerPlate.Core.UserManagement.Commands.ChangePasswordUser;
using BoilerPlate.Core.UserManagement.Commands.CreateUser;
using BoilerPlate.Core.UserManagement.Commands.EditUser;
using BoilerPlate.Core.UserManagement.Queries.GetUsers;
using BoilerPlate.Shared.Abstraction.Queries;
using BoilerPlate.Shared.Infrastructure.Primitives;
using Microsoft.AspNetCore.Authorization;

namespace BoilerPlate.Api.Controllers;

[Authorize]
public sealed class UserManagementController : BaseController
{
    /// <summary>
    /// Create User API
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command,
        CancellationToken cancellationToken)
        => Result.Success(command)
            .Bind(x => Sender.Send(x, cancellationToken))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Edit User API
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> EditUserAsync([FromBody] EditUserCommand command, CancellationToken cancellationToken)
        => Result.Success(command).Bind(e => Sender.Send(e, cancellationToken))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Change Password User API
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("users/password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> ChangePasswordUserAsync([FromBody] ChangePasswordUserCommand command,
        CancellationToken cancellationToken)
        => Result.Success(command).Bind(e => Sender.Send(e, cancellationToken))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Get Users API
    /// </summary>
    /// <param name="username"></param>
    /// <param name="fullName"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <returns></returns>
    [HttpGet("users")]
    [ProducesResponseType(typeof(PagedList<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRaceStatus(
        string? username,
        string? fullName,
        int page,
        int pageSize,
        string orderBy) =>
        Ok(await Sender.Send(new GetUsersQuery(page, pageSize, orderBy, fullName, username)));
}