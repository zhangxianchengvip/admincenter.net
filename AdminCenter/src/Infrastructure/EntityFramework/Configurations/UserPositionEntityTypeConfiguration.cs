using AdminCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminCenter.Infrastructure.EntityFramework.Configurations;
internal class UserPositionEntityTypeConfiguration : IEntityTypeConfiguration<UserPosition>
{
    public void Configure(EntityTypeBuilder<UserPosition> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.PositionId });
    }
}
