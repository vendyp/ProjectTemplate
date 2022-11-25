using BoilerPlate.Core.Abstractions;
using BoilerPlate.Core.Identity.Commands.ChangePassword;
using BoilerPlate.IntegrationTests.Fixtures;
using BoilerPlate.Shared.Abstraction.Databases;
using BoilerPlate.Shared.Abstraction.Storage;
using BoilerPlate.Shared.Abstraction.Time;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace BoilerPlate.IntegrationTests.Handlers;

/// <summary>
/// Using independent state
/// </summary>
[Collection("change-password")]
public class ChangePasswordHandlerTests : IClassFixture<ServiceFixture>
{
    public const string Username = "admin";
    public const string Password = "Qwerty@1234";
    private readonly ChangePasswordCommandHandler _ctor;
    private readonly ServiceProvider _serviceProvider;

    public ChangePasswordHandlerTests(ServiceFixture serviceFixture)
    {
        _serviceProvider = serviceFixture.ServiceProvider;
        _ctor = new ChangePasswordCommandHandler(serviceFixture.ServiceProvider.GetRequiredService<IUserService>(),
            serviceFixture.ServiceProvider.GetRequiredService<IClock>(),
            serviceFixture.ServiceProvider.GetRequiredService<IDbContext>(),
            serviceFixture.ServiceProvider.GetRequiredService<IRequestStorage>());
    }

    [Fact]
    public async Task ChangePassword_WithInvalidUserId_Should_Return_Failed_With_NotFound_Message()
    {
        var command = new ChangePasswordCommand()
        {
            CurrentPassword = "abcd",
            NewPassword = "abcde",
            ForceReLogin = false
        };
        command.SetUserId(Guid.NewGuid());

        var result = await _ctor.Handle(command, CancellationToken.None);

        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe("ExCP001");
    }

    [Fact]
    public async Task ChangePassword_WithInvalidPassword_Should_Return_Failed_With_InvalidPassword_Message()
    {
        var command = new ChangePasswordCommand()
        {
            CurrentPassword = "abcd",
            NewPassword = "abcde",
            ForceReLogin = false
        };
        command.SetUserId(Guid.NewGuid());

        var result = await _ctor.Handle(command, CancellationToken.None);

        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe("ExCP001");
    }

    [Fact]
    public async Task ChangePassword_WithCorrectPassword_Should_Return_Success()
    {
        var command = new ChangePasswordCommand()
        {
            CurrentPassword = Password,
            NewPassword = "Qwerty@12345",
            ForceReLogin = false
        };
        command.SetUserId(Guid.Empty);

        var result = await _ctor.Handle(command, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();

        var userService = _serviceProvider.GetRequiredService<IUserService>();
        var user = await userService.GetUserByIdAsync(command.GetUserId(), CancellationToken.None);
        user.ShouldNotBeNull();
        userService.VerifyPassword(user.Password!, command.NewPassword).ShouldBeTrue();
    }
}