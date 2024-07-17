﻿using System.Diagnostics.CodeAnalysis;
using AdminCenter.Domain.Constants;
using Ardalis.GuardClauses;

namespace AdminCenter.Domain;

/// <summary>
/// 用户角色
/// </summary>
public class UserRole : AuditableEntity
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public required Guid UserId { get; set; }

    /// <summary>
    /// 角色Id
    /// </summary>
    public required Guid RoleId { get; set; }
}
