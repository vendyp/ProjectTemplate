using BoilerPlate.Core.UserManagement;
using BoilerPlate.Core.UserManagement.Commands.CreateUser;
using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Entities;
using BoilerPlate.UnitTests.Mocks;
using BoilerPlate.UnitTests.UserManagement.CreateUser.DataTests;
using Moq;

namespace BoilerPlate.UnitTests.UserManagement.CreateUser;

public class CreateUserCommandHandlerTests
{
    /// <summary>
    /// Test create user command handler with correct parameters return user already registered
    /// </summary>
    /// <param name="command"></param>
    [Theory]
    [ClassData(typeof(CorrectDataTestForCreateUserCommandHandler))]
    public async Task CreateUserCommandHandlerShouldReturnUserAlreadyRegistered(CreateUserCommand command)
    {
        var mockDbContext = new DbContextBuilder().Build();
        //get username mocked to not return null
        var mockUserService = new UserServiceBuilder()
            .SetupGetUsernameAsyncAsAny(new User())
            .Build();
        var mockPasswordHasher = new PasswordHasherUserBuilder().Build();
        var mockRoleService = new RoleServiceBuilder().Build();

        var ctr = new CreateUserCommandHandler(mockDbContext.Object, mockUserService.Object, mockPasswordHasher.Object,
            mockRoleService.Object);
        var result = await ctr.Handle(command, CancellationToken.None);

        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ValidationErrors.UserManagementErrors.UserAlreadyRegistered);
    }

    /// <summary>
    /// Test create user command handler return ok on correct flow
    /// </summary>
    /// <param name="command"></param>
    [Theory]
    [ClassData(typeof(CorrectDataTestForCreateUserCommandHandler))]
    public async Task CreateUserCommandHandlerShouldReturnSuccessOnCorrectFlow(CreateUserCommand command)
    {
        var mockDbContext = new DbContextBuilder().Build();
        //get username mocked to return null
        var mockUserService = new UserServiceBuilder()
            .SetupGetUsernameAsyncAsAny(null)
            .Build();
        //setup hash password pass command.Password
        var mockPasswordHasher = new PasswordHasherUserBuilder()
            .SetupHashPassword(default!, command.Password)
            .Build();
        var mockRoleService = new RoleServiceBuilder()
            .SetupGetRoleOfAdministrator()
            .SetupGetRoleOfUser()
            .Build();

        var ctr = new CreateUserCommandHandler(mockDbContext.Object, mockUserService.Object, mockPasswordHasher.Object,
            mockRoleService.Object);

        //verify user that passed to insert was correct input data
        mockDbContext.Setup(e => e.Insert(It.IsAny<User>()))
            .Callback<User>(e =>
            {
                e.Username.ShouldBe(command.Username.ToLower());
                e.FullName.ShouldBe(command.Fullname);
                e.Password.ShouldNotBeNullOrWhiteSpace();

                e.UserRoles.ShouldNotBeEmpty();
            });

        var result = await ctr.Handle(command, CancellationToken.None);

        //verify insert called once
        mockDbContext.Verify(e => e.Insert(It.IsAny<BaseEntity>()), Times.Once);
        //verify save changes called once
        mockDbContext.Verify(e => e.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        //verify that returned value IsFailure should be false
        result.IsFailure.ShouldBeFalse();
    }
}