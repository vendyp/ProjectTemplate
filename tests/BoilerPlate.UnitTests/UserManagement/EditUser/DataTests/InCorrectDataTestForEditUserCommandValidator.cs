using BoilerPlate.Core.UserManagement.EditUser;

namespace BoilerPlate.UnitTests.UserManagement.EditUser.DataTests;

public class InCorrectDataTestForEditUserCommandValidator : IEnumerable<object[]>
{
    private static readonly Random Random = new();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    public InCorrectDataTestForEditUserCommandValidator()
    {
        Data = new List<object[]>();

        for (var i = 0; i < 100; i++)
            Data.Add(new object[]
            {
                new EditUserCommand
                {
                    FullName = RandomString(Random.Next(100, 200)),
                    AboutMe = RandomString(Random.Next(1000, 5000))
                }
            });
    }

    private List<object[]> Data { get; }

    public IEnumerator<object[]> GetEnumerator() => Data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}