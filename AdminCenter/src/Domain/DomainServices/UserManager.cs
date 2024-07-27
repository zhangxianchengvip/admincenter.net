using System.Diagnostics.CodeAnalysis;
using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Domain;

public class UserManager(IApplicationDbContext context) : DomainEvent
{
    /// <summary>
    /// 创建用户
    /// </summary>
    public async Task<User> CreateAsync(
        [NotNull] string loginName,
        [NotNull] string realName,
        [NotNull] string password,
        string? nickName,
        string? email,
        string? phoneNumber,
        [NotNull] List<Guid> roleIds,
        [NotNull] List<(Guid organizationId, bool isSubsidiary)> organizations)
    {
        var user = new User
        (
            id: Guid.NewGuid(),
            loginName: loginName,
            password: password,
            realName: realName,
            nickName: nickName,
            phoneNumber: phoneNumber,
            email: email
        );

        user.UpdateRoleRange(roleIds);

        user.UpdateOrganizationRange(organizations);

        var exist = await context.Users.AnyAsync(s => s.LoginName.Equals(loginName));

        return !exist ? user : throw new BusinessException(ExceptionMessage.UserExist);
    }
}
