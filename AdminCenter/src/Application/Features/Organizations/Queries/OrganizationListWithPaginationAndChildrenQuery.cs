using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Features.Organizations.Dtos;
using AdminCenter.Application.Features.Organizations.Extensions;
using AdminCenter.Domain.Common.Repository;

namespace AdminCenter.Application.Features.Organizations.Queries;

public record class OrganizationListWithPaginationAndChildrenQuery(int PageNumber, int PageSize) : IRequest<PaginatedList<OrganizationWithChildrenDto>>;

public class OrganizationListWithPaginationAndChildrenQueryHandler(IApplicationDbContext context) : IRequestHandler<OrganizationListWithPaginationAndChildrenQuery, PaginatedList<OrganizationWithChildrenDto>>
{
    public async Task<PaginatedList<OrganizationWithChildrenDto>> Handle(OrganizationListWithPaginationAndChildrenQuery request, CancellationToken cancellationToken)
    {
        var organizationList = await context.Organizations
        .ToListAsync();

        var result = organizationList
        .BuildOrganizationTree()
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize).ToList();

        return new PaginatedList<OrganizationWithChildrenDto>(result, result.Count, request.PageNumber, request.PageSize);
    }
}

