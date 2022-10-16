namespace BoilerPlate.Core.UserManagement.Commands.EditUser;

public class EditUserCommandHandler : ICommandHandler<EditUserCommand, Result>
{
    private readonly IDbContext _dbContext;
    private readonly IUserService _userService;

    public EditUserCommandHandler(IDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public async Task<Result> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return Result.Failure(ValidationErrors.UserManagementErrors.UserNotFound);

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