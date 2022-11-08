using BoilerPlate.Core.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace BoilerPlate.Core.UserManagement.Commands.ChangePasswordUser;

public class ChangePasswordUserCommandHandler : ICommandHandler<ChangePasswordUserCommand, Result>
{
    private readonly IServiceProvider _serviceProvider;

    public ChangePasswordUserCommandHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async ValueTask<Result> Handle(ChangePasswordUserCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
        
        var user = await userService.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return Result.Failure(UserManagementErrors.UserNotFoundInChangePassword);

        user.Password = passwordHasher.HashPassword(null!, request.NewPassword);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}