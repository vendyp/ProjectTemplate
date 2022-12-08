using BoilerPlate.Core.Responses;

namespace BoilerPlate.Core.Identity.Queries.GetMe;

public sealed record GetMeQuery : IQuery<Result<MeResponse?>>
{
    public Guid UserId { get; set; }
}