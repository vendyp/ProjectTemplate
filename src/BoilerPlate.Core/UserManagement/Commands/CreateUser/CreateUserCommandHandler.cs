using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Commands;
using BoilerPlate.Shared.Abstraction.Databases;
using BoilerPlate.Shared.Abstraction.Primitives;
using Microsoft.AspNetCore.Identity;

namespace BoilerPlate.Core.UserManagement.Commands.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result>
{
    private readonly IDbContext _dbContext;
    private readonly IUserService _userService;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IRoleService _roleService;

    public CreateUserCommandHandler(IDbContext dbContext, IUserService userService,
        IPasswordHasher<User> passwordHasher, IRoleService roleService)
    {
        _dbContext = dbContext;
        _userService = userService;
        _passwordHasher = passwordHasher;
        _roleService = roleService;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByUsernameAsync(request.NormalizedUsername, cancellationToken);
        if (user is not null)
            return Result.Failure(ValidationErrors.UserManagementErrors.UserAlreadyRegistered);

        var roleOfUser = await _roleService.GetRoleOfUserAsync(cancellationToken);

        user = new User
        {
            Username = request.Username.ToLower(),
            FullName = request.Fullname,
            Password = _passwordHasher.HashPassword(default!, request.Password)
        };

        user.UserRoles.Add(new UserRole { RoleId = roleOfUser!.RoleId });

        _dbContext.Insert(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}