using System.Diagnostics.CodeAnalysis;
using AdminCenter.Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Domain;

public class OrganizationManager(IApplicationDbContext context) : DomainService
{
    public async Task<Organization> UpdateAsync(
        [NotNull] Guid id,
        [NotNull] string name,
        [NotNull] string code,
        string description,
        Guid? superiorOrganizationId)
    {
        if (await context.Organizations.AnyAsync(s => s.Name.Equals(name)))
            throw new Exception();

        if (await context.Organizations.AnyAsync(s => s.Code.Equals(code)))
            throw new Exception();

        var organizatoin = await context.Organizations.FindAsync(id);

        Guard.Against.Null(organizatoin);

        organizatoin.Description = description;
        organizatoin.SuperiorOrganizationId = superiorOrganizationId;
        organizatoin.UpdateOrganizationName(name).UpdateOrganizationCode(code);

        return organizatoin;
    }
}
