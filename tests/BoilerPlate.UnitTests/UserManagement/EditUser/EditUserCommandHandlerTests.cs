using BoilerPlate.Core.UserManagement;
using BoilerPlate.Core.UserManagement.Commands.EditUser;
using BoilerPlate.UnitTests.Mocks;

namespace BoilerPlate.UnitTests.UserManagement.EditUser;

public class EditUserCommandHandlerTests
{
    [Fact]
    public async Task TestEditUserCommandHandlerShouldReturnFailureWhenUserNotFound()
    {
        var dbContext = new DbContextBuilder().Build();
        var userService = new UserServiceBuilder().SetupGetUserByIdAsyncAsAny(null).Build();

        var handler = new EditUserCommandHandler(dbContext.Object, userService.Object);

        var result = await handler.Handle(new EditUserCommand() { UserId = Guid.NewGuid() }, CancellationToken.None);

        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(UserManagementErrors.UserNotFound.Code);
    }
}