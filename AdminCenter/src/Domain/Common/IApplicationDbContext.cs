
using AdminCenter.Domain;
using AdminCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Application.Common.Interfaces;

/// <summary>
/// 仓储
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// 用户
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// 角色
    /// </summary>
    DbSet<Role> Roles { get; }

    /// <summary>
    /// 职位
    /// </summary>
    DbSet<Position> Positions { get; }

    /// <summary>
    /// 组织
    /// </summary>
    DbSet<Organization> Organizations { get; }

    /// <summary>
    /// 菜单
    /// </summary>
    DbSet<Menu> Menus { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
