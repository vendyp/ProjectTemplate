namespace BoilerPlate.Shared.Abstraction.Auth;

public interface IRequirement
{
    string Policy { get;  }
    string Permission { get;  }
}