﻿using BoilerPlate.Shared.Abstraction.Entities;

namespace BoilerPlate.Domain.Entities;

public class Role : BaseEntity
{
    public const string RoleAdminId = "00000000-0000-0000-0000-000000000000";
    public const string RoleUserId = "655af068-a673-4bef-b69c-2938f2614fb4";
    public const string RoleUserName = "user";
    public const string RoleUserAdminName = "admin";

    public Role()
    {
        RoleId = Guid.NewGuid();
        RolePermissions = new HashSet<RolePermission>();
    }

    /// <summary>
    /// Primary key of an object
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// Abbreviation of it role full name
    /// </summary>
    public string Code { get; set; } = default!;

    /// <summary>
    /// Full name of it role
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Upper case of role name
    /// </summary>
    public string NormalizedName { get; set; } = default!;

    public ICollection<RolePermission> RolePermissions { get; }
}