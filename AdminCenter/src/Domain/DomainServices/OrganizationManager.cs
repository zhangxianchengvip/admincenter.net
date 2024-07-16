using System.Diagnostics.CodeAnalysis;
using AdminCenter.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Domain;

public class OrganizationManager(IApplicationDbContext context)
{

    public async Task<Organization> UpdateAsync(
        [NotNull] Guid id,
        [NotNull] string name,
        [NotNull] string code,
        string description,
        Guid? superiorOrganizationId)

    {
        var exit = await context.Organizations.AnyAsync(s => s.Name == name || s.Code == code);

        if (exit) throw new ArgumentOutOfRangeException(nameof(name));

        var organizatoin = await context.Organizations.FindAsync(id);

        if (organizatoin is null)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }

        organizatoin.UpdateOrganizationName(name).UpdateOrganizationCode(code);
        organizatoin.SuperiorOrganizationId = superiorOrganizationId;
        organizatoin.Description = description;

        return organizatoin;
    }

}
