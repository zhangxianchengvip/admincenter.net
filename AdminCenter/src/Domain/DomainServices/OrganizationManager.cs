using System.Diagnostics.CodeAnalysis;
using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Domain;

public class OrganizationManager(IApplicationDbContext context) : DomainService
{
    public async Task<Organization> UpdateAsync(
        [NotNull] Organization organization,
        [NotNull] string name,
        [NotNull] string code,
        string? description = null,
        Guid? superiorOrganizationId = null)
    {
        if (await context.Organizations.AnyAsync(s => s.Code.Equals(code)))
        {
            throw new AdminBusinessException(ExctptionMessage.OrganizationCodeExist);
        }

        organization.Description = description;
        organization.SuperiorOrganizationId = superiorOrganizationId;
        organization.UpdateOrganizationName(name).UpdateOrganizationCode(code);

        return organization;
    }
}
