using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Features.Roles.Dto;
using CleanArchitecture.Application.Common.Mappings;

namespace AdminCenter.Application.Features.Roles.Queries;

/// <summary>
/// 角色列表
/// </summary>
public record RoleListWithPaginationQuery(int PageNumber, int PageSize) : IRequest<PaginatedList<RoleDto>>;
public class RoleListWithPaginationQueryValidator : AbstractValidator<RoleListWithPaginationQuery>
{
    public RoleListWithPaginationQueryValidator()
    {
        RuleFor(v => v.PageNumber).GreaterThan(0);
        RuleFor(v => v.PageSize).GreaterThan(5).LessThan(50);
    }
}

public class RoleListWithPaginationQueryValidatorHandler(IApplicationDbContext context) : IRequestHandler<RoleListWithPaginationQuery, PaginatedList<RoleDto>>
{
    public async Task<PaginatedList<RoleDto>> Handle(RoleListWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await context.Roles
        .OrderByDescending(s => s.Order)
        .ProjectToType<RoleDto>()
        .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
