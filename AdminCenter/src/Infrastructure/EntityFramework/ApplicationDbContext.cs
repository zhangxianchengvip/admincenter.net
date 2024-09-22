using System.Reflection;
using AdminCenter.Domain;
using AdminCenter.Domain.Common.Repository;
using AdminCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Infrastructure.EntityFramework;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Organization> Organizations => Set<Organization>();

    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<Position> Positions => Set<Position>();

    public DbSet<Menu> Menus => Set<Menu>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
