using BoilerPlate.Shared.Abstraction.Auth;
using Moq;

namespace BoilerPlate.UnitTests.Extensions;

public static class AuthManagerBuilderExtensions
{
    public static Mock<IAuthManager> Create()
    {
        var mock = new Mock<IAuthManager>();
        return mock;
    }
}