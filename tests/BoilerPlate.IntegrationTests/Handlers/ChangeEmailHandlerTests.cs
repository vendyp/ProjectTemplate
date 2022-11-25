using BoilerPlate.Core.Abstractions;
using BoilerPlate.Core.Identity.Commands.ChangeEmail;
using BoilerPlate.Domain.ValueObjects;
using BoilerPlate.IntegrationTests.Fixtures;
using BoilerPlate.Shared.Abstraction.Databases;
using BoilerPlate.Shared.Abstraction.Encryption;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace BoilerPlate.IntegrationTests.Handlers;

[Collection(Constant.ServiceCollectionDefaultName)]
public class ChangeEmailHandlerTests : IClassFixture<ServiceFixture>
{
    private readonly ChangeEmailCommandHandler _ctor;
    private readonly ServiceProvider _serviceProvider;

    public ChangeEmailHandlerTests(ServiceFixture serviceFixture)
    {
        _serviceProvider = serviceFixture.ServiceProvider;
        _ctor = new ChangeEmailCommandHandler(
            serviceFixture.ServiceProvider.GetRequiredService<IUserService>(),
            serviceFixture.ServiceProvider.GetRequiredService<IRng>(),
            serviceFixture.ServiceProvider.GetRequiredService<IDbContext>());
    }

    [Fact]
    public async Task ChangeEmail_WithInvalidParameter_ShouldReturn_Failed()
    {
        var command = new ChangeEmailCommand
        {
            NewEmail = "test@test.com"
        };
        command.SetUserId(Guid.NewGuid());

        var result = await _ctor.Handle(command, CancellationToken.None);

        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe("ExCE001");
    }

    [Fact]
    public async Task ChangeEmail_WithCorrectParameter_ShouldReturn_Success()
    {
        var command = new ChangeEmailCommand
        {
            NewEmail = "test@test.com"
        };
        command.SetUserId(Guid.Empty);

        var result = await _ctor.Handle(command, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();
        result.Error.Code.ShouldBe(string.Empty);

        var userService = _serviceProvider.GetRequiredService<IUserService>();
        var user = await userService.GetUserByIdAsync(Guid.Empty, CancellationToken.None);
        user.ShouldNotBeNull();
        user.Email.ShouldBe(new Email(command.NewEmail));
    }
}