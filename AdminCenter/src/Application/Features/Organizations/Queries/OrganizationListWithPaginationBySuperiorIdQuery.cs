using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Features.Organizations.Dto;
using CleanArchitecture.Application.Common.Mappings;

namespace AdminCenter.Application.Features.Organizations.Queries;

/// <summary>
/// 获取下级组织
/// </summary>
/// <param name="Id"></param>
public record OrganizationListWithPaginationBySuperiorIdQuery(Guid? SuperiorId, int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<OrganizationDto>>;

public class OrganizationListBySuperiorOrganizationIdHandler(IApplicationDbContext context) : IRequestHandler<OrganizationListWithPaginationBySuperiorIdQuery, PaginatedList<OrganizationDto>>
{
    public async Task<PaginatedList<OrganizationDto>> Handle(OrganizationListWithPaginationBySuperiorIdQuery request, CancellationToken cancellationToken)
    {
        return await context.Organizations
        .Where(s => s.SuperiorId.Equals(request.SuperiorId))
        .ProjectToType<OrganizationDto>()
        .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
