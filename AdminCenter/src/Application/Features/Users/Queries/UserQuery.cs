using AdminCenter.Application.Features.Users.Dto;
using AdminCenter.Domain.Common.Repository;

namespace AdminCenter.Application.Features.Users.Queries;

/// <summary>
/// 用户信息查询
/// </summary>
public record UserQuery(Guid Id) : IRequest<UserDto>;

public class UserQueryHandler(IApplicationDbContext context) : IRequestHandler<UserQuery, UserDto>
{
    public async Task<UserDto> Handle(UserQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.Id);

        return user != null ? user.Adapt<UserDto>() : throw new BusinessException(ExceptionMessage.UserNotExist);
    }
}
