namespace BoilerPlate.Shared.Abstraction.Encryption;

public interface IHasher
{
    string Hash(string data);
}