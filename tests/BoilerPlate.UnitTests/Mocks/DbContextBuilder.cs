using BoilerPlate.Shared.Abstraction.Databases;
using Moq;

namespace BoilerPlate.UnitTests.Mocks;

public class DbContextBuilder
{
    private readonly Mock<IDbContext> _mockDbContext;

    public DbContextBuilder()
    {
        _mockDbContext = new Mock<IDbContext>();
    }

    public Mock<IDbContext> Build()
    {
        return _mockDbContext;
    }
}