using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain;
using Microsoft.AspNetCore.Identity;

namespace BoilerPlate.Core.UserManagement.Commands.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result>
{
    private readonly IUserService _userService;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IDbContext _dbContext;

    public CreateUserCommandHandler(IUserService userService, IPasswordHasher<User> passwordHasher,
        IDbContext dbContext)
    {
        _userService = userService;
        _passwordHasher = passwordHasher;
        _dbContext = dbContext;
    }

    public async ValueTask<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByUsernameAsync(request.NormalizedUsername, cancellationToken);
        if (user is not null)
            return Result.Failure(Error.Create("ExCU001", "User already registered."));

        user = new User
        {
            Username = request.Username.ToLower(),
            FullName = request.Fullname,
            Password = _passwordHasher.HashPassword(default!, request.Password)
        };

        user.UserRoles.Add(new UserRole { RoleId = RoleConstant.User });

        _dbContext.Insert(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}