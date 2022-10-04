namespace BoilerPlate.Shared.Abstraction.Encryption;

public interface IMd5
{
    string Calculate(string value);
}