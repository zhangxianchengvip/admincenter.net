
using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Application.Users.Dto;
using AdminCenter.Domain.Constants;
using AdminCenter.Domain.Exceptions;
using Mapster;

namespace AdminCenter.Application;

/// <summary>
/// 登录查询
/// </summary>
/// <param name="LoginName">账号</param>
/// <param name="Password">密码</param>
public record UserLoginQuery(string LoginName, string Password) : IRequest<UserDto>;


public class UserLoginHandler(IApplicationDbContext context) : IRequestHandler<UserLoginQuery, UserDto>
{
    public async Task<UserDto> Handle(UserLoginQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(s => s.LoginName.Equals(request.LoginName));

        if (user == null)
        {
            throw new AdminBusinessException(ExctptionMessage.UserNotExist);
        }

        if (!user.ValidatePassword(request.Password))
        {
            throw new AdminBusinessException(ExctptionMessage.UserPasswordError);
        }

        return user.Adapt<UserDto>();
    }
}
