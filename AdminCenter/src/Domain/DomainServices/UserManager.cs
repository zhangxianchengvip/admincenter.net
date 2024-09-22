using System.Diagnostics.CodeAnalysis;
using AdminCenter.Domain.Common.Domain;
using AdminCenter.Domain.Common.Repository;
using AdminCenter.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Domain;

public class UserManager(IApplicationDbContext context) : DomainService
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
            roles: roleIds,
            organizations: organizations,
            nickName: nickName,
            phoneNumber: phoneNumber,
            email: email
        );

        // 验证用户是否存在
        var exist = await context.Users.AnyAsync(s => s.LoginName.Equals(loginName));

        return !exist ? user : throw new BusinessException(ExceptionMessage.UserExist);
    }
}
