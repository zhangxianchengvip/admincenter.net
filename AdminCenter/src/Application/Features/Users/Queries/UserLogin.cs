using AdminCenter.Application.Features.Users.Dto;

namespace AdminCenter.Application.Features.Users.Queries;

/// <summary>
/// 登录查询
/// </summary>
/// <param name="LoginName">账号</param>
/// <param name="Password">密码</param>
public record UserLogin(string Account, string Password) : IRequest<UserDto>;

public class UserLoginQueryValidator : AbstractValidator<UserLogin>
{
    public UserLoginQueryValidator()
    {
        RuleFor(v => v.Account).NotNull();
        RuleFor(v => v.Password).NotNull();
    }
}
public class UserLoginHandler(IApplicationDbContext context) : IRequestHandler<UserLogin, UserDto>
{
    public async Task<UserDto> Handle(UserLogin request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(s => s.LoginName.Equals(request.Account));

        if (user == null) throw new BusinessException(ExceptionMessage.UserNotExist);

        return user.ValidatePassword(request.Password) ? user.Adapt<UserDto>() : throw new BusinessException(ExceptionMessage.UserPasswordError);
    }
}
