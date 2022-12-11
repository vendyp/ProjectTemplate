using BoilerPlate.Shared.Abstraction.Storage;
using Moq;

namespace BoilerPlate.UnitTests.Extensions;

public static class RequestStorageBuilderExtensions
{
    public static Mock<IRequestStorage> Create()
    {
        var mock = new Mock<IRequestStorage>();
        return mock;
    }
}