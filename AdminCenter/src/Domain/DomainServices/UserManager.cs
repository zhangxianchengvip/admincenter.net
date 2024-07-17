using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Domain;

public class UserManager(IApplicationDbContext context) : DomainEvent
{
    public async Task<User> CreateAsync(
    string loginName,
    string realName,
    string password,
    string? nickName,
    string? email,
    string? phoneNumber,
    List<Guid> roleIds,
    List<(Guid SuperiorOrganizationId, bool isSubsidiary)> superiorOrganizationIds)
    {

        var exist = await context.Users.AnyAsync
        (
            s => s.LoginName == loginName
        );

        if (exist) throw new AdminBusinessException(ExctptionMessage.UserExist);


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

        return user;
    }
}
