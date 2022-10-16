using BoilerPlate.Core.Identity.Commands.SignIn;

namespace BoilerPlate.Api.Controllers;

public sealed class IdentityController : BaseController
{
    public IdentityController(ISender sender) : base(sender)
    {
    }

    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRace([FromBody] SignInCommand request)
    {
        var result = await Sender.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}