namespace BoilerPlate.Core.UserManagement.Queries.GetUserById;

public sealed class GetUserByIdQuery : IQuery<Maybe<UserDetailResponse>>
{
    public Guid UserId { get; set; }
}