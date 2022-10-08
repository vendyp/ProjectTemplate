using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain.Entities;
using Moq;

namespace BoilerPlate.UnitTests.Mocks;

public class RoleServiceBuilder
{
    private readonly Mock<IRoleService> _mock;

    public RoleServiceBuilder()
    {
        _mock = new Mock<IRoleService>();
    }

    public RoleServiceBuilder SetupGetRoleOfAdministrator()
    {
        var role = new Role();
        _mock.Setup(e => e.GetRoleOfAdministratorAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(role);
        return this;
    }

    public RoleServiceBuilder SetupGetRoleOfUser()
    {
        var role = new Role();
        _mock.Setup(e => e.GetRoleOfUserAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(role);
        return this;
    }

    public Mock<IRoleService> Build()
    {
        return _mock;
    }
}