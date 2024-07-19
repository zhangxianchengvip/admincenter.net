using AdminCenter.Application.Features.Users.Dto;

namespace AdminCenter.Application.Features.Users.Queries;

/// <summary>
/// 登录查询
/// </summary>
/// <param name="LoginName">账号</param>
/// <param name="Password">密码</param>
public record UserLoginQuery(string LoginName, string Password) : IRequest<UserDto>;

public class UserLoginQueryValidator : AbstractValidator<UserLoginQuery>
{
    public UserLoginQueryValidator()
    {
        RuleFor(v => v.LoginName).NotNull();
        RuleFor(v => v.Password).NotNull();
    }
}
public class UserLoginHandler(IApplicationDbContext context) : IRequestHandler<UserLoginQuery, UserDto>
{
    public async Task<UserDto> Handle(UserLoginQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(s => s.LoginName.Equals(request.LoginName));

        if (user == null)
        {
            throw new BusinessException(ExceptionMessage.UserNotExist);
        }

        if (!user.ValidatePassword(request.Password))
        {
            throw new BusinessException(ExceptionMessage.UserPasswordError);
        }

        return user.Adapt<UserDto>();
    }
}
