using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Databases;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Infrastructure.Services;

public class ModuleService : IModuleService
{
    private readonly IDbContext _dbContext;

    public ModuleService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Module?> GetModuleByIdAsync(Guid id, CancellationToken cancellationToken)
        => _dbContext.Set<Module>().Include(e => e.Permissions)
            .Include(e => e.SubModules)
            .ThenInclude(e => e.Permissions)
            .Where(e => e.ModuleId == id)
            .FirstOrDefaultAsync(cancellationToken);
}