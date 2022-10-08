using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain.Entities;
using Moq;

namespace BoilerPlate.UnitTests.Mocks;

public class UserServiceBuilder
{
    private readonly Mock<IUserService> _mock;

    public UserServiceBuilder()
    {
        _mock = new Mock<IUserService>();
    }

    public UserServiceBuilder SetupGetUserByIdAsyncAsAny(User? callBackResult)
    {
        _mock.Setup(e => e.GetUserByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(callBackResult);
        return this;
    }

    public UserServiceBuilder SetupGetUsernameAsyncAsAny(User? callBackResult)
    {
        _mock.Setup(e => e.GetUserByUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(callBackResult);
        return this;
    }

    public Mock<IUserService> Build()
    {
        return _mock;
    }
}