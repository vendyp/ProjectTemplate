namespace BoilerPlate.Core.Abstractions;

public interface IModuleService
{
    Task<Module?> GetModuleByIdAsync(Guid id, CancellationToken cancellationToken);
}