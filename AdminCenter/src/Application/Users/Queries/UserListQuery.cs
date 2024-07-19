using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Users.Dto;
using CleanArchitecture.Application.Common.Mappings;

namespace AdminCenter.Application.Users.Queries;

/// <summary>
/// 用户列表查询
/// </summary>
/// <param name="PageNumber"></param>
/// <param name="PageSize"></param>
public record UserListQuery(int PageNumber, int PageSize) : IRequest<PaginatedList<UserDto>>;


public class UserListQueryHandler(IApplicationDbContext context) : IRequestHandler<UserListQuery, PaginatedList<UserDto>>
{
    public async Task<PaginatedList<UserDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        return await context.Users
        .ProjectToType<UserDto>()
        .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
