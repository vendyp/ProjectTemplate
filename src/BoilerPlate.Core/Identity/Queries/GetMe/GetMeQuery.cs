using BoilerPlate.Core.Contracts;

namespace BoilerPlate.Core.Identity.Queries.GetMe;

public sealed class GetMeQuery : IQuery<Maybe<MeResponse?>>
{
    public Guid UserId { get; set; }
}