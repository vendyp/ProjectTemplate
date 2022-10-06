using BoilerPlate.Core.UserManagement.CreateUser;

namespace BoilerPlate.UnitTests.UserManagement.CreateUser;

public class CreateUserCommandTests
{
    [Fact]
    public void TestConstructCreateUserCommandShouldBeNotException()
    {
        _ = new CreateUserCommand();
    }

    [Theory]
    [ClassData(typeof(CorrectDataTestCreateUserCommand))]
    public void TestCreateUserCommandValidatorShouldBePassed(CreateUserCommand command)
    {
        var validator = new CreateUserCommandValidator();
        var result = validator.Validate(command);

        result.IsValid.ShouldBeTrue();
    }
}