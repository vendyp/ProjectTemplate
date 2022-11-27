using BoilerPlate.App.Models;

namespace BoilerPlate.App.Abstractions;

public interface IAccountService
{
    User? User { get; }
}