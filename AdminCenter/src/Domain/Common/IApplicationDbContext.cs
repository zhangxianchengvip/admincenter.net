
using AdminCenter.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Application.Common.Interfaces;

/// <summary>
/// 仓储
/// </summary>
public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<Position> Positions { get; }
    DbSet<Organization> Organizations { get; }
}
