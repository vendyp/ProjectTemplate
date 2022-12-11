using BoilerPlate.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BoilerPlate.UnitTests.Extensions;

public static class PasswordHasherUserBuilderExtensions
{
    public static Mock<IPasswordHasher<User>> Create()
    {
        var mock = new Mock<IPasswordHasher<User>>();
        return mock;
    }
}