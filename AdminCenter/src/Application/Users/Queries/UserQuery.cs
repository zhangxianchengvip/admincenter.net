using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Application.Users.Dto;
using AdminCenter.Domain.Constants;
using AdminCenter.Domain.Exceptions;
using Mapster;

namespace AdminCenter.Application.Users.Queries;

/// <summary>
/// 用户信息查询
/// </summary>
/// <param name="Id"></param>
public record UserQuery(Guid Id) : IRequest<UserDto>;

public class UserQueryHandler(IApplicationDbContext context) : IRequestHandler<UserQuery, UserDto>
{
    public async Task<UserDto> Handle(UserQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.Id);

        if (user != null) return user.Adapt<UserDto>();

        throw new AdminBusinessException(ExctptionMessage.UserNotExist);
    }
}
