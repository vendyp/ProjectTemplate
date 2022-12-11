using BoilerPlate.Core.Abstractions;
using Moq;

namespace BoilerPlate.UnitTests.Extensions;

public static class UserServiceBuilderExtensions
{
    public static Mock<IUserService> Create()
    {
        var mock = new Mock<IUserService>();
        return mock;
    }
}