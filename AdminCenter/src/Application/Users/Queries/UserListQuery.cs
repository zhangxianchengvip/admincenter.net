using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Users.Dto;
using CleanArchitecture.Application.Common.Mappings;

namespace AdminCenter.Application.Users.Queries;

/// <summary>
/// 用户列表查询
/// </summary>
public record UserListQuery(int PageNumber, int PageSize) : IRequest<PaginatedList<UserDto>>;

public class UserListQueryValidator : AbstractValidator<UserListQuery>
{
    public UserListQueryValidator()
    {
        RuleFor(v => v.PageNumber).GreaterThan(0);
        RuleFor(v => v.PageSize).GreaterThan(5).LessThan(50);
    }
}
public class UserListQueryHandler(IApplicationDbContext context) : IRequestHandler<UserListQuery, PaginatedList<UserDto>>
{
    public async Task<PaginatedList<UserDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        return await context.Users
        .ProjectToType<UserDto>()
        .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
