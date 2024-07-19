using System.Diagnostics.CodeAnalysis;
using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Domain.DomainServices;
public class RoleManager(IApplicationDbContext context) : DomainService
{
    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    /// <exception cref="AdminBusinessException"></exception>
    public async Task<Role> CreateAsync([NotNull] string name, string? description = null)
    {
        var role = new Role
        (
            id: Guid.NewGuid(),
            name: name,
            description: description
        );

        if (!await context.Roles.AnyAsync(s => s.Name.Equals(name)))
        {
            return role;
        }

        throw new AdminBusinessException(ExceptionMessage.RoleExist);
    }

    /// <summary>
    /// 角色修改
    /// </summary>
    /// <param name="role"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    /// <exception cref="AdminBusinessException"></exception>
    public async Task<Role> UpdateAsync([NotNull] Role role, [NotNull] string name, string? description = null)
    {
        role.Description = description;
        role.UpdateRoleName(name);

        if (!await context.Roles.AnyAsync(s => s.Name.Equals(name)))
        {
            return role;
        }

        throw new AdminBusinessException(ExceptionMessage.RoleExist);
    }
}
