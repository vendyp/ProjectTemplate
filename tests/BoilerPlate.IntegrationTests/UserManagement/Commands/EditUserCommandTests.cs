using BoilerPlate.Core.UserManagement.Commands.EditUser;
using BoilerPlate.Infrastructure.Services;
using BoilerPlate.IntegrationTests.Fixtures;
using Shouldly;

namespace BoilerPlate.IntegrationTests.UserManagement.Commands;

[Collection("EditUserCommandCollection")]
public class EditUserCommandTests : IClassFixture<SqlServerDatabaseFixture>
{
    private readonly SqlServerDatabaseFixture _fixture;

    public EditUserCommandTests(SqlServerDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestEditUserCommandOnlyChangeFullNameShouldBeCorrect()
    {
        var userService = new UserService(_fixture.DbContext);
        //get administrator user, this will not null
        var user = (await userService.GetUserByIdAsync(Guid.Empty, CancellationToken.None))!;

        //verify that current fullname is not same as data test
        const string aboutToChange = "Not Administrator";
        user.FullName.ShouldNotBe(aboutToChange);

        var editCommand = new EditUserCommand { UserId = user.UserId, FullName = aboutToChange };
        var ctr = new EditUserCommandHandler(_fixture.DbContext, userService);
        var result = await ctr.Handle(editCommand, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();

        var user2 = (await userService.GetUserByIdAsync(Guid.Empty, CancellationToken.None))!;
        user2.FullName.ShouldBe(aboutToChange);
    }

    [Fact]
    public async Task TestEditUserCommandOnlyChangeAboutMeShouldBeCorrect()
    {
        var userService = new UserService(_fixture.DbContext);
        //get administrator user, this will not null
        var user = (await userService.GetUserByIdAsync(Guid.Empty, CancellationToken.None))!;
        const string aboutToChange = "About me";
        user.AboutMe.ShouldNotBe(aboutToChange);

        var editCommand = new EditUserCommand { UserId = user.UserId, AboutMe = aboutToChange };
        var ctr = new EditUserCommandHandler(_fixture.DbContext, userService);
        var result = await ctr.Handle(editCommand, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();

        var user2 = (await userService.GetUserByIdAsync(Guid.Empty, CancellationToken.None))!;

        user2.AboutMe.ShouldBe(aboutToChange);
    }
}