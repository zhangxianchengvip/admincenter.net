using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Roles.Dto;
using CleanArchitecture.Application.Common.Mappings;

namespace AdminCenter.Application.Roles.Queries;

/// <summary>
/// 角色列表
/// </summary>
public record RoleListQuery(int PageNumber, int PageSize) : IRequest<PaginatedList<RoleDto>>;
public class RoleListQueryValidator : AbstractValidator<RoleListQuery>
{
    public RoleListQueryValidator()
    {
        RuleFor(v => v.PageNumber).GreaterThan(0);
        RuleFor(v => v.PageSize).GreaterThan(5).LessThan(50);
    }
}

public class RoleListQueryHandler(IApplicationDbContext context) : IRequestHandler<RoleListQuery, PaginatedList<RoleDto>>
{
    public async Task<PaginatedList<RoleDto>> Handle(RoleListQuery request, CancellationToken cancellationToken)
    {
        return await context.Roles
        .ProjectToType<RoleDto>()
        .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
