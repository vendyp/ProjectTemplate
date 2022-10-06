using BoilerPlate.Shared.Abstraction.Commands;
using BoilerPlate.Shared.Abstraction.Databases;
using BoilerPlate.Shared.Abstraction.Primitives;

namespace BoilerPlate.Core.UserManagement.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result>
{
    private readonly IDbContext _dbContext;

    public CreateUserCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}