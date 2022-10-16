namespace BoilerPlate.Core.Identity.Queries;

public sealed class GetMeQuery : IQuery<Maybe<MeResponse?>>
{
    public Guid UserId { get; set; }
}