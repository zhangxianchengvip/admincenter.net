using AdminCenter.Domain;
using AdminCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminCenter.Infrastructure.EntityFramework.Configurations;
internal class RoleMenuEntityTypeConfiguration : IEntityTypeConfiguration<RoleMenu>
{
    public void Configure(EntityTypeBuilder<RoleMenu> builder)
    {
        builder.HasKey(ur => new { ur.MenuId, ur.RoleId });
    }
}
