using BoilerPlate.Shared.Abstraction.Contexts;
using Moq;

namespace BoilerPlate.UnitTests.Mocks;

public class ContextBuilder
{
    private readonly Mock<IContext> _mock;

    public ContextBuilder()
    {
        _mock = new Mock<IContext>();
        _mock.Setup(e => e.RequestId).Returns(Guid.NewGuid);
        _mock.Setup(e => e.CorrelationId).Returns(Guid.NewGuid);
        _mock.Setup(e => e.TraceId).Returns(Guid.NewGuid().ToString);
        _mock.Setup(e => e.IpAddress).Returns((string?)null);
        _mock.Setup(e => e.UserAgent).Returns((string?)null);
        var test = new Mock<IIdentityContext>();
        test.Setup(e => e.IsAuthenticated).Returns(false);
        test.Setup(e => e.Id).Returns(Guid.NewGuid);
        test.Setup(e => e.Username).Returns(string.Empty);
        test.Setup(e => e.Claims).Returns(new Dictionary<string, IEnumerable<string>>());
        test.Setup(e => e.Roles).Returns(new List<string>());
        _mock.Setup(e => e.Identity).Returns(test.Object);
    }

    public Mock<IContext> Build() => _mock;
}