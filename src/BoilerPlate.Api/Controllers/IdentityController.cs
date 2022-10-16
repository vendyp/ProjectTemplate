using BoilerPlate.Core.Identity.Commands.SignIn;
using BoilerPlate.Core.Identity.Queries;
using Microsoft.AspNetCore.Authorization;

namespace BoilerPlate.Api.Controllers;

public sealed class IdentityController : BaseController
{
    /// <summary>
    /// Sign In API
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignInAsync([FromBody] SignInCommand request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    /// <summary>
    /// Get Me API
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMeAsync(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetMeQuery { UserId = Context!.Identity.Id }, cancellationToken);
        return result.HasValue ? Ok(result.Value!) : BadRequest();
    }
}