using BoilerPlate.Shared.Abstraction.Time;
using Moq;

namespace BoilerPlate.UnitTests.Extensions;

public static class ClockBuilderExtensions
{
    public static Mock<IClock> Create()
    {
        var mock = new Mock<IClock>();

        mock.Setup(e => e.CurrentDate()).Returns(DateTime.Now);
        mock.Setup(e => e.CurrentServerDate()).Returns(DateTime.Now);

        return mock;
    }
}