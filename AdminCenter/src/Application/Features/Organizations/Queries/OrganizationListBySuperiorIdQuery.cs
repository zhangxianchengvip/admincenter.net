using AdminCenter.Application.Features.Organizations.Dto;
using AdminCenter.Domain.Common.Repository;

namespace AdminCenter.Application.Features.Organizations.Queries;

public record OrganizationListBySuperiorIdQuery(Guid? SuperiorId) : IRequest<List<OrganizationDto>>;


public class OrganizationListBySuperiorIdQueryHandler(IApplicationDbContext context) : IRequestHandler<OrganizationListBySuperiorIdQuery, List<OrganizationDto>>
{
    public async Task<List<OrganizationDto>> Handle(OrganizationListBySuperiorIdQuery request, CancellationToken cancellationToken)
    {
        return await context.Organizations
        .Where(s => s.SuperiorId == request.SuperiorId)
        .ProjectToType<OrganizationDto>()
        .ToListAsync();
    }
}
