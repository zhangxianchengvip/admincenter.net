
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
public record UserLogin(string LoginName, string Password) : IRequest<UserDto>;


public class UserLoginHandler(IApplicationDbContext context) : IRequestHandler<UserLogin, UserDto>
{
    public async Task<UserDto> Handle(UserLogin request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(s => s.LoginName.Equals(request.LoginName));

        if (user == null)
        {
            throw new AdminBusinessException(ExctptionMessage.UserNotExist);
        }

        if (user.ValidatePassword(request.Password))
        {
            return user.Adapt<UserDto>();
        }

        throw new AdminBusinessException(ExctptionMessage.UserPasswordError);
    }
}
