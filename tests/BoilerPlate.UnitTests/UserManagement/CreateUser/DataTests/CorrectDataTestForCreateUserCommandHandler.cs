using BoilerPlate.Core.UserManagement.CreateUser;

namespace BoilerPlate.UnitTests.UserManagement.CreateUser.DataTests;

public class CorrectDataTestForCreateUserCommandHandler : IEnumerable<object[]>
{
    public CorrectDataTestForCreateUserCommandHandler()
    {
        Data = new List<object[]>
        {
            new object[]
            {
                new CreateUserCommand
                    { Username = "tESt@test.com", Fullname = "Dolor Ipsum", Password = "qwerty@123" }
            }
        };
    }

    private List<object[]> Data { get; }

    public IEnumerator<object[]> GetEnumerator() => Data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}