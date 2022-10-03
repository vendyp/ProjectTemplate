namespace BoilerPlate.Shared.Abstraction.Databases;

public interface IInitializer
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}