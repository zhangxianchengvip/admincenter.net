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
public record UserInfoQuery(Guid Id) : IRequest<UserDto>;

public class UserInfoHandler(IApplicationDbContext context) : IRequestHandler<UserInfoQuery, UserDto>
{
    public async Task<UserDto> Handle(UserInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.Id);

        if (user is null)
        {
            throw new AdminBusinessException(ExctptionMessage.UserNotExist);
        }

        return user.Adapt<UserDto>();
    }
}
