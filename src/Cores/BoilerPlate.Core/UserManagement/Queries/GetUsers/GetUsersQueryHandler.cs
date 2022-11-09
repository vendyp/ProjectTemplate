using System.Linq.Dynamic.Core;
using BoilerPlate.Core.Contracts;

namespace BoilerPlate.Core.UserManagement.Queries.GetUsers;

public sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, PagedList<UserResponse>>
{
    private readonly IServiceProvider _serviceProvider;

    public GetUsersQueryHandler(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async ValueTask<PagedList<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();

        var queryable = dbContext.Set<User>().AsNoTracking().AsQueryable();

        if (request.Username.IsNotNullOrWhiteSpace())
            queryable = queryable.Where(e => EF.Functions.Like(e.Username, $"%{request.Username}%"));

        if (request.FullName.IsNotNullOrWhiteSpace())
            queryable = queryable.Where(e => EF.Functions.Like(e.FullName, $"{request.FullName}"));

        if (request.OrderBy.IsNotNullOrWhiteSpace())
            queryable = queryable.OrderBy(request.OrderBy);

        var projection = queryable.Select(e => new UserResponse
        {
            UserId = e.UserId,
            Username = e.Username,
            FullName = e.FullName,
            UserState = e.UserState,
            CreatedAt = e.CreatedAt,
            CreatedAtServer = e.CreatedAtServer
        }).AsQueryable();

        var users = await projection
            .Skip(request.CalculateSkip())
            .Take(request.Size)
            .ToListAsync(cancellationToken);

        var totalCount = await projection.LongCountAsync(cancellationToken);

        var response = new PagedList<UserResponse>(users, totalCount, request.Page, request.Size);

        return response;
    }
}