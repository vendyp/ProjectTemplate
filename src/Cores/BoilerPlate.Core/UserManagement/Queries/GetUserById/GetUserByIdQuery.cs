using BoilerPlate.Core.Contracts;

namespace BoilerPlate.Core.UserManagement.Queries.GetUserById;

public sealed record GetUserByIdQuery : IQuery<Maybe<UserDetailResponse>>
{
    public GetUserByIdQuery()
    {
    }

    public GetUserByIdQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
}