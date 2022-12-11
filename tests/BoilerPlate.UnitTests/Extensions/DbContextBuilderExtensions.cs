using BoilerPlate.Shared.Abstraction.Databases;
using Moq;

namespace BoilerPlate.UnitTests.Extensions;

public static class DbContextBuilderExtensions
{
    public static Mock<IDbContext> Create()
    {
        var mockDbContext = new Mock<IDbContext>();
        return mockDbContext;
    }
}