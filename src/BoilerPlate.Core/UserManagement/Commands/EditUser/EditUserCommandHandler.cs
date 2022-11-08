using BoilerPlate.Core.Abstractions;

namespace BoilerPlate.Core.UserManagement.Commands.EditUser;

public class EditUserCommandHandler : ICommandHandler<EditUserCommand, Result>
{
    private readonly IServiceProvider _serviceProvider;

    public EditUserCommandHandler(IServiceProvider serviceProvider)
    => _serviceProvider = serviceProvider;

    public async ValueTask<Result> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
        
        var user = await userService.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return Result.Failure(UserManagementErrors.UserNotFound);

        if (request.FullName.IsNotNullOrWhiteSpace())
            if (user.FullName != request.FullName)
                user.FullName = request.FullName!;

        if (request.AboutMe.IsNotNullOrWhiteSpace())
            if (user.AboutMe != request.AboutMe)
                user.AboutMe = request.AboutMe;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}