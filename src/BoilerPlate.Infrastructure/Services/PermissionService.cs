using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Databases;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Infrastructure.Services;

internal class PermissionService : IPermissionService
{
    private readonly IDbContext _dbContext;

    public PermissionService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Permission?> GetPermissionByIdAsync(string id, CancellationToken cancellationToken)
        => _dbContext.Set<Permission>().Where(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async Task<bool> AllIdIsValid(string[] ids, CancellationToken cancellationToken)
    {
        var listId = ids.Distinct().ToList();
        if (listId.Count != ids.Length)
            throw new InvalidOperationException("Result may have duplicate value");

        var listPermission = await _dbContext.Set<Permission>().AsNoTracking().Where(e => listId.Contains(e.Id))
            .ToListAsync(cancellationToken);

        if (!listPermission.Any()) return false;

        return listPermission.Count == listId.Count;
    }
}