using BoilerPlate.Core.Responses;

namespace BoilerPlate.Core.UserManagement.Queries.GetUserById;

public sealed record GetUserByIdQuery : IQuery<Result<UserDetailResponse>>
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