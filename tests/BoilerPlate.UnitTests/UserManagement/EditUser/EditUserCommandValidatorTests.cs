using BoilerPlate.Core.UserManagement.Commands.EditUser;
using BoilerPlate.UnitTests.UserManagement.EditUser.DataTests;

namespace BoilerPlate.UnitTests.UserManagement.EditUser;

public class EditUserCommandValidatorTests
{
    [Theory]
    [ClassData(typeof(InCorrectDataTestForEditUserCommandValidator))]
    public void TestEditUserCommandValidatorShouldBeFalse(EditUserCommand command)
    {
        var validator = new EditUserCommandValidator();
        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
    }
}