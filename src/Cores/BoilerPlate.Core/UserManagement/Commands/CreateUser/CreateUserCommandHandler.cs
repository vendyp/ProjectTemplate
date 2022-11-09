using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain;
using Microsoft.AspNetCore.Identity;

namespace BoilerPlate.Core.UserManagement.Commands.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result>
{
    private readonly IServiceProvider _serviceProvider;

    public CreateUserCommandHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async ValueTask<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();

        var user = await userService.GetUserByUsernameAsync(request.NormalizedUsername, cancellationToken);
        if (user is not null)
            return Result.Failure(UserManagementErrors.UserAlreadyRegistered);

        user = new User
        {
            Username = request.Username.ToLower(),
            FullName = request.Fullname,
            Password = passwordHasher.HashPassword(default!, request.Password)
        };

        user.UserRoles.Add(new UserRole { RoleId = RoleConstant.User });

        dbContext.Insert(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}