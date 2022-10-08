using BoilerPlate.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BoilerPlate.UnitTests.Mocks;

public class PasswordHasherUserBuilder
{
    private readonly Mock<IPasswordHasher<User>> _mock;

    public PasswordHasherUserBuilder()
    {
        _mock = new Mock<IPasswordHasher<User>>();
    }

    public PasswordHasherUserBuilder SetupHashPassword(User user, string password)
    {
        _mock.Setup(e => e.HashPassword(It.IsAny<User>(), password))
            .Returns("test");
        return this;
    }

    public Mock<IPasswordHasher<User>> Build()
    {
        return _mock;
    }
}