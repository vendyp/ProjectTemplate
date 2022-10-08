using BoilerPlate.Core.Abstractions;
using BoilerPlate.Domain.Entities;
using BoilerPlate.Shared.Abstraction.Commands;
using BoilerPlate.Shared.Abstraction.Databases;
using BoilerPlate.Shared.Abstraction.Primitives;
using Microsoft.AspNetCore.Identity;

namespace BoilerPlate.Core.UserManagement.Commands.ChangePasswordUser;

public class ChangePasswordUserCommandHandler : ICommandHandler<ChangePasswordUserCommand, Result>
{
    private readonly IDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserService _userService;

    public ChangePasswordUserCommandHandler(IDbContext dbContext, IPasswordHasher<User> passwordHasher,
        IUserService userService)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _userService = userService;
    }

    public async Task<Result> Handle(ChangePasswordUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return Result.Failure(ValidationErrors.UserManagementErrors.UserNotFoundInChangePassword);

        user.Password = _passwordHasher.HashPassword(null!, request.NewPassword);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}