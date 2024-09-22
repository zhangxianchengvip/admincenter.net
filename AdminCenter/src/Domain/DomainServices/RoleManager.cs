using System.Diagnostics.CodeAnalysis;
using AdminCenter.Domain.Common.Domain;
using AdminCenter.Domain.Common.Repository;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Domain.DomainServices;
public class RoleManager(IApplicationDbContext context) : DomainService
{
    /// <summary>
    /// 创建角色
    /// </summary>
    public async Task<Role> CreateAsync([NotNull] string roleName, [NotNull] string showName, int order, string? description = null)
    {
        var role = new Role
        (
            id: Guid.NewGuid(),
            roleName: roleName,
            showName: showName,
            order: order,
            description: description
        );

        var exist = await context.Roles.AnyAsync(s => s.RoleName.Equals(roleName));

        return !exist ? role : throw new BusinessException("角色已存在");
    }

    /// <summary>
    /// 角色修改
    /// </summary>
    public async Task<Role> UpdateAsync([NotNull] Role role, [NotNull] string roleName, [NotNull] string showName, int order, string? description = null)
    {
        role.Order = order;
        role.Description = description;
        role.UpdateRoleName(roleName);
        role.UpdateShowName(showName);

        var exist = await context.Roles.AnyAsync(s => s.RoleName.Equals(roleName));

        return !exist ? role : throw new BusinessException("角色已存在");
    }
}
