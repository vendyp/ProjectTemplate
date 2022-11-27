using BoilerPlate.App.Abstractions;
using BoilerPlate.App.Models;

namespace BoilerPlate.App.Services;

public class AccountService : IAccountService
{
    public AccountService()
    {
        User = null;
    }

    public User? User { get; }
}