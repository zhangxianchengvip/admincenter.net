using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Users.Dto;
using CleanArchitecture.Application.Common.Mappings;
using Mapster;

namespace AdminCenter.Application.Users.Queries;

/// <summary>
/// 用户列表查询
/// </summary>
/// <param name="PageNumber"></param>
/// <param name="PageSize"></param>
public record GetUserInfoListQuery(int PageNumber, int PageSize) : IRequest<PaginatedList<UserDto>>;


public class GetUserInfoListHandler(IApplicationDbContext context) : IRequestHandler<GetUserInfoListQuery, PaginatedList<UserDto>>
{
    public async Task<PaginatedList<UserDto>> Handle(GetUserInfoListQuery request, CancellationToken cancellationToken)
    {
        return await context.Users
             .ProjectToType<UserDto>()
             .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
