using AdminCenter.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminCenter.Infrastructure.EntityFramework.Configurations;
internal class UserOrganizationEntityTypeConfiguration : IEntityTypeConfiguration<UserOrganization>
{
    public void Configure(EntityTypeBuilder<UserOrganization> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.OrganizationId });
    }
}
