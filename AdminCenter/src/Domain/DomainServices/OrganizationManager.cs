using System.Diagnostics.CodeAnalysis;
using AdminCenter.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Domain;

public class OrganizationManager(IRepository<Organization, Guid> organizationRepository)
{

    public async Task<Organization> Update(
        [NotNull] Guid id,
        [NotNull] string name,
        [NotNull] string code,
        string description,
        Guid? superiorOrganizationId)

    {
        var exit = await organizationRepository.Data
            .AnyAsync(s => s.Name == name || s.Code == code);

        if (exit) throw new ArgumentOutOfRangeException(nameof(exit));

        var organizatoin = await organizationRepository.Data.FindAsync(id);

        if (organizatoin == null)
        {
            throw new ArgumentOutOfRangeException(nameof(exit));
        }

        organizatoin.UpdateOrganizationName(name)
                    .UpdateOrganizationCode(code);

        organizatoin.SuperiorOrganizationId = superiorOrganizationId;
        organizatoin.Description = description;

        return organizatoin;
    }

}
