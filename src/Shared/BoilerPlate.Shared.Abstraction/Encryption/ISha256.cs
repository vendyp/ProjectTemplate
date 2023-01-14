namespace BoilerPlate.Shared.Abstraction.Encryption;

public interface ISha256
{
    string Hash(string data);
}