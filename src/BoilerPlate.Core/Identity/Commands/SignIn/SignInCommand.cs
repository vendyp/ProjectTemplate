using BoilerPlate.Shared.Abstraction.Auth;

namespace BoilerPlate.Core.Identity.Commands.SignIn;

public sealed class SignInCommand : ICommand<Result<JsonWebToken>>
{
    private DeviceType _deviceType;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    public void SetDeviceType(DeviceType deviceType)
    {
        _deviceType = deviceType;
    }

    public DeviceType GetDeviceType() => _deviceType;
}