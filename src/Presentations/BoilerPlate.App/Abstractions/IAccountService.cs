using BoilerPlate.App.Models;

namespace BoilerPlate.App.Abstractions;

public interface IAccountService
{
    public User? User { get; }
}