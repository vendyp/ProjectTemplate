﻿using BoilerPlate.Domain.Entities;
using BoilerPlate.Domain.Entities.Enums;
using BoilerPlate.Shared.Abstraction.Databases;
using BoilerPlate.Shared.Abstraction.Time;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Domain.Services;

public class ApplicationInitializer : IInitializer
{
    private readonly IDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IClock _clock;

    public ApplicationInitializer(IDbContext dbContext, IPasswordHasher<User> passwordHasher, IClock clock)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _clock = clock;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await AddRoleUserAsync(cancellationToken);

        await AddRoleAdminAsync(cancellationToken);

        await AddUserAdminAsync(cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task AddRoleAdminAsync(CancellationToken cancellationToken)
    {
        var roleAdminId = new Guid(Role.RoleAdminId);
        if (await _dbContext.Set<Role>().AnyAsync(e => e.RoleId == roleAdminId, cancellationToken: cancellationToken))
            return;

        await _dbContext.Set<Role>().AddAsync(new Role
        {
            CreatedBy = null,
            CreatedByName = "system",
            CreatedAt = _clock.CurrentServerDate(),
            RoleId = roleAdminId,
            Code = "adm",
            Name = Role.RoleUserAdminName,
            NormalizedName = Role.RoleUserAdminName.ToUpper(),
        }, cancellationToken);
    }

    private async Task AddRoleUserAsync(CancellationToken cancellationToken)
    {
        var roleUserId = new Guid(Role.RoleUserId);
        if (await _dbContext.Set<Role>().AnyAsync(e => e.RoleId == roleUserId, cancellationToken))
            return;

        await _dbContext.Set<Role>().AddAsync(new Role
        {
            CreatedBy = null,
            CreatedByName = "system",
            CreatedAt = _clock.CurrentServerDate(),
            RoleId = roleUserId,
            Code = "usr",
            Name = Role.RoleUserName,
            NormalizedName = Role.RoleUserName.ToUpper(),
        }, cancellationToken);
    }

    private async Task AddUserAdminAsync(CancellationToken cancellationToken)
    {
        if (await _dbContext.Set<User>().AnyAsync(e => e.UserId == Guid.Empty, cancellationToken: cancellationToken))
            return;

        await _dbContext.Set<User>().AddAsync(new User
        {
            UserId = Guid.Empty,
            Username = "admin",
            NormalizedUsername = "admin".ToUpper(),
            Password = _passwordHasher.HashPassword(default!, "Qwerty@1234"),
            FullName = "Administrator",
            UserState = UserState.Active,
            CreatedAt = _clock.CurrentServerDate(),
            CreatedByName = "system"
        }, cancellationToken);

        await _dbContext.Set<UserRole>().AddAsync(new UserRole
        {
            UserId = Guid.Empty,
            RoleId = new Guid(Role.RoleAdminId)
        }, cancellationToken);
    }
}