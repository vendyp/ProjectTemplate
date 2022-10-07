using Bogus;
using BoilerPlate.Core.UserManagement.CreateUser;

namespace BoilerPlate.UnitTests.UserManagement.CreateUser.DataTests;

/// <summary>
/// Create data test of create user command with username max length 100, fullname max length 100, and password max length 35
/// </summary>
public class CorrectDataTestCreateUserCommand : IEnumerable<object[]>
{
    public CorrectDataTestCreateUserCommand()
    {
        Data = new List<object[]>();

        var fakerCommand = new Faker<CreateUserCommand>(locale: "id_ID")
            //Basic rules using built-in generators
            .RuleFor(u => u.Username, (f, _) => f.Internet.Email())
            .RuleFor(u => u.Fullname, (f, _) => $"{f.Name.FirstName()} {f.Name.LastName()}")
            .RuleFor(u => u.Password, (f, _) => f.Internet.Password(length: 35));
        var commands = fakerCommand.GenerateLazy(256);
        foreach (var item in commands.Where(e =>
                     e.Username.Length is >= 4 and <= 100 && e.Fullname.Length is >= 4 and <= 100))
            Data.Add(new object[] { item });
    }

    private List<object[]> Data { get; }

    public IEnumerator<object[]> GetEnumerator() => Data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}