using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Roles.Dto;
using CleanArchitecture.Application.Common.Mappings;

namespace AdminCenter.Application.Roles.Queries;
public record RoleListQuery(int PageNumber, int PageSize) : IRequest<PaginatedList<RoleDto>>;

public class RoleListQueryHandler(IApplicationDbContext context) : IRequestHandler<RoleListQuery, PaginatedList<RoleDto>>
{
    public async Task<PaginatedList<RoleDto>> Handle(RoleListQuery request, CancellationToken cancellationToken)
    {
        return await context.Roles
        .ProjectToType<RoleDto>()
        .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
