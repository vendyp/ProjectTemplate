using BoilerPlate.Core.Abstractions;

namespace BoilerPlate.Core.UserManagement.Commands.EditUser;

public class EditUserCommandHandler : ICommandHandler<EditUserCommand, Result>
{
    private readonly IUserService _userService;
    private readonly IDbContext _dbContext;

    public EditUserCommandHandler(IUserService userService, IDbContext dbContext)
    {
        _userService = userService;
        _dbContext = dbContext;
    }

    public async ValueTask<Result> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return Result.Failure(Error.Create("ExEU001", "Data not found."));

        if (request.FullName.IsNotNullOrWhiteSpace())
            if (user.FullName != request.FullName)
                user.FullName = request.FullName!;

        if (request.AboutMe.IsNotNullOrWhiteSpace())
            if (user.AboutMe != request.AboutMe)
                user.AboutMe = request.AboutMe;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}