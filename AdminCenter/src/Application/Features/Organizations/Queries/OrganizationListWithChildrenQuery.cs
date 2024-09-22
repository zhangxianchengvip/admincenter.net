using AdminCenter.Application.Features.Organizations.Dtos;
using AdminCenter.Application.Features.Organizations.Extensions;
using AdminCenter.Domain;
using AdminCenter.Domain.Common.Repository;

namespace AdminCenter.Application.Features.Organizations.Queries;

public record class OrganizationListWithChildrenQuery : IRequest<List<OrganizationWithChildrenDto>>;


public class OrganizationListWithChildrenQueryHandler(IApplicationDbContext context) : IRequestHandler<OrganizationListWithChildrenQuery, List<OrganizationWithChildrenDto>>
{
    public async Task<List<OrganizationWithChildrenDto>> Handle(OrganizationListWithChildrenQuery request, CancellationToken cancellationToken)
    {
        var organizations = await context.Organizations
        .ToListAsync(cancellationToken);
        
        return organizations.BuildOrganizationTree();
    }
}
