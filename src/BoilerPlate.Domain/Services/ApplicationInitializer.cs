using BoilerPlate.Shared.Abstraction.Databases;

namespace BoilerPlate.Domain.Services;

public class ApplicationInitializer : IInitializer
{
    private readonly IDbContext _dbContext;

    public ApplicationInitializer(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task ExecuteAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}