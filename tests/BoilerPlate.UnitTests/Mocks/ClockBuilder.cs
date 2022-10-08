using BoilerPlate.Shared.Abstraction.Time;
using Moq;

namespace BoilerPlate.UnitTests.Mocks;

public class ClockBuilder
{
    private readonly Mock<IClock> _mock;

    public ClockBuilder()
    {
        _mock = new Mock<IClock>();

        _mock.Setup(e => e.CurrentDate()).Returns(new DateTime());
        _mock.Setup(e => e.CurrentServerDate()).Returns(new DateTime());
    }

    public Mock<IClock> Build() => _mock;
}