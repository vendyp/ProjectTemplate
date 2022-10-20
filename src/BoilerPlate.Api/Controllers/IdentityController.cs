using BoilerPlate.Core.Identity.Commands.ChangeEmail;
using BoilerPlate.Core.Identity.Commands.ChangePassword;
using BoilerPlate.Core.Identity.Commands.RefreshToken;
using BoilerPlate.Core.Identity.Commands.SignIn;
using BoilerPlate.Core.Identity.Commands.VerifyEmail;
using BoilerPlate.Core.Identity.Queries.GetMe;
using BoilerPlate.Shared.Infrastructure.Primitives;
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

    /// <summary>
    /// Change Password User API
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("password")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordCommand command,
        CancellationToken cancellationToken)
    {
        command.SetUserId(Context!.Identity.Id);

        return await Result.Success(command)
            .Bind(x => Sender.Send(x, cancellationToken))
            .Match(Ok, BadRequest);
    }

    /// <summary>
    /// Refresh Token API
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    /// <summary>
    /// Change Email API
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("email")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> ChangeEmailAsync([FromBody] ChangeEmailCommand command,
        CancellationToken cancellationToken)
        => Result.Success(command.SetUserId(Context!.Identity.Id))
            .Bind(x => Sender.Send(x, cancellationToken))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Verify Email API
    /// </summary>
    /// <param name="code"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("verify-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> VerifyEmailAsync([FromQuery] string code,
        CancellationToken cancellationToken)
        => Result.Success(new VerifyEmailCommand { Code = code })
            .Bind(x => Sender.Send(x, cancellationToken))
            .Match(Ok, BadRequest);
}