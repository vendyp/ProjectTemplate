using BoilerPlate.Core.Abstractions;
using BoilerPlate.Core.Identity.Commands.ChangeEmail;
using BoilerPlate.Core.Identity.Commands.VerifyEmail;
using BoilerPlate.Domain.Entities.Enums;
using BoilerPlate.IntegrationTests.Fixtures;
using BoilerPlate.Shared.Abstraction.Databases;
using BoilerPlate.Shared.Abstraction.Encryption;
using BoilerPlate.Shared.Abstraction.Time;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace BoilerPlate.IntegrationTests.Handlers;

[Collection(Constant.ServiceCollectionDefaultName)]
public class VerifyEmailHandlerTests : IClassFixture<ServiceFixture>
{
    private readonly VerifyEmailCommandHandler _ctor;
    private readonly ServiceProvider _serviceProvider;

    public VerifyEmailHandlerTests(ServiceFixture serviceFixture)
    {
        _serviceProvider = serviceFixture.ServiceProvider;
        _ctor = new VerifyEmailCommandHandler(
            serviceFixture.ServiceProvider.GetRequiredService<IDbContext>(),
            serviceFixture.ServiceProvider.GetRequiredService<IClock>());
    }

    [Fact]
    public async Task VerifyEmail_When_InvalidParameter_Should_Return_Success()
    {
        var changeEmailCommand = new ChangeEmailCommand
        {
            NewEmail = "test@test.com"
        };
        changeEmailCommand.SetUserId(Guid.Empty);
        var changeEmailHandler = new ChangeEmailCommandHandler(_serviceProvider.GetRequiredService<IUserService>(),
            _serviceProvider.GetRequiredService<IRng>(),
            _serviceProvider.GetRequiredService<IDbContext>());
        await changeEmailHandler.Handle(changeEmailCommand, CancellationToken.None);

        var userService = _serviceProvider.GetRequiredService<IUserService>();
        var user = (await userService.GetUserByIdAsync(Guid.Empty, CancellationToken.None))!;
        user.EmailActivationCode.ShouldNotBeNullOrEmpty();
        user.EmailActivationStatus.ShouldBe(EmailActivationStatus.NeedActivation);

        var verifyEmailCommand = new VerifyEmailCommand
        {
            Code = user.EmailActivationCode!
        };
        var result = await _ctor.Handle(verifyEmailCommand, CancellationToken.None);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task VerifyEmail_When_InvalidParameter_Should_Return_Failed()
    {
        var verifyEmailCommand = new VerifyEmailCommand
        {
            Code = Guid.NewGuid().ToString()
        };
        var result = await _ctor.Handle(verifyEmailCommand, CancellationToken.None);
        result.IsFailure.ShouldBeTrue();
    }
}