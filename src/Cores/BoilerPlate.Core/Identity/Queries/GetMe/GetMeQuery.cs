using BoilerPlate.Core.Contracts;

namespace BoilerPlate.Core.Identity.Queries.GetMe;

public sealed record GetMeQuery : IQuery<Maybe<MeResponse?>>
{
    public Guid UserId { get; set; }
}