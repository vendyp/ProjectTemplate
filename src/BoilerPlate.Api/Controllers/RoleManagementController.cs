using BoilerPlate.Core.RoleManagement;
using BoilerPlate.Core.RoleManagement.Commands.CreateRole;
using BoilerPlate.Core.RoleManagement.Commands.EditRole;
using BoilerPlate.Shared.Infrastructure.Primitives;
using Microsoft.AspNetCore.Authorization;

namespace BoilerPlate.Api.Controllers;

[Authorize(Policy = RoleManagementConstant.PermissionRead)]
public sealed class RoleManagementController : BaseController
{
    /// <summary>
    /// Create Role API
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("roles")]
    [Authorize(Policy = RoleManagementConstant.PermissionWrite)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateUserAsync([FromBody] CreateRoleCommand command,
        CancellationToken cancellationToken)
        => Result.Success(command)
            .Bind(x => Sender.Send(x, cancellationToken))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Edit Role API
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("roles")]
    [Authorize(Policy = RoleManagementConstant.PermissionWrite)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> EditUserAsync([FromBody] EditRoleCommand command, CancellationToken cancellationToken)
        => Result.Success(command).Bind(e => Sender.Send(e, cancellationToken))
            .Match(Ok, BadRequest);
}