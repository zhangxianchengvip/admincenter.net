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
    public async Task<Organization> CreateAsync(
        [NotNull] string name,
        [NotNull] string code,
        string? description = null,
        Guid? superiorId = null)
    {
        var organization = new Organization
        (
            id: Guid.NewGuid(),
            name: name,
            code: code,
            description: description,
            superiorId: superiorId
        );

        var exist = await context.Organizations.AnyAsync(s => s.Code.Equals(code));

        return !exist ? organization : throw new BusinessException(ExceptionMessage.OrganizationCodeExist);
    }

    /// <summary>
    /// 修改组织
    /// </summary>
    public async Task<Organization> UpdateAsync(
        [NotNull] Organization organization,
        [NotNull] string name,
        [NotNull] string code,
        string? description = null,
        Guid? superiorId = null)
    {
        organization.Description = description;
        organization.UpdateOrganizationName(name);
        organization.UpdateOrganizationCode(code);
        organization.SuperiorId = superiorId;

        var exist = await context.Organizations.AnyAsync(s => s.Code.Equals(code));

        return !exist ? organization : throw new BusinessException(ExceptionMessage.OrganizationCodeExist);
    }
}
