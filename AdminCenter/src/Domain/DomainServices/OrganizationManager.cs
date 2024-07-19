using System.Diagnostics.CodeAnalysis;
using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Domain;

public class OrganizationManager(IApplicationDbContext context) : DomainService
{

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="name"></param>
    /// <param name="code"></param>
    /// <param name="description"></param>
    /// <param name="superiorOrganizationId"></param>
    /// <returns></returns>
    /// <exception cref="AdminBusinessException"></exception>
    public async Task<Organization> CreateAsync(
        [NotNull] string name,
        [NotNull] string code,
        string? description = null,
        Guid? superiorOrganizationId = null)
    {
        var organization = new Organization
        (
            id: Guid.NewGuid(),
            name: name,
            code: code,
            description: description,
            superiorOrganizationId: superiorOrganizationId
        );

        if (!await context.Organizations.AnyAsync(s => s.Code.Equals(code)))
        {
            return organization;
        }

        throw new AdminBusinessException(ExceptionMessage.OrganizationCodeExist);
    }

    /// <summary>
    /// 修改组织
    /// </summary>
    /// <param name="organization"></param>
    /// <param name="name"></param>
    /// <param name="code"></param>
    /// <param name="description"></param>
    /// <param name="superiorOrganizationId"></param>
    /// <returns></returns>
    /// <exception cref="AdminBusinessException"></exception>
    public async Task<Organization> UpdateAsync(
        [NotNull] Organization organization,
        [NotNull] string name,
        [NotNull] string code,
        string? description = null,
        Guid? superiorOrganizationId = null)
    {
        organization.UpdateOrganizationName(name);
        organization.UpdateOrganizationCode(code);
        organization.Description = description;
        organization.SuperiorOrganizationId = superiorOrganizationId;

        if (!await context.Organizations.AnyAsync(s => s.Code.Equals(code)))
        {
            return organization;
        }

        throw new AdminBusinessException(ExceptionMessage.OrganizationCodeExist);
    }
}
