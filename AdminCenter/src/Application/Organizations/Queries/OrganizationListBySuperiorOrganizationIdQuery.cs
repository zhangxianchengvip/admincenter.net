using AdminCenter.Application.Common.Interfaces;
using Mapster;

namespace AdminCenter.Application;

/// <summary>
/// 获取下级组织
/// </summary>
/// <param name="Id"></param>
public record OrganizationListBySuperiorOrganizationIdQuery(Guid? Id) : IRequest<List<OrganizationDto>>;

public class OrganizationListBySuperiorOrganizationIdHandler(IApplicationDbContext context) : IRequestHandler<OrganizationListBySuperiorOrganizationIdQuery, List<OrganizationDto>>
{
    public async Task<List<OrganizationDto>> Handle(OrganizationListBySuperiorOrganizationIdQuery request, CancellationToken cancellationToken)
    {
        return await context.Organizations
        .Where(s => s.SuperiorOrganizationId.Equals(request.Id))
        .ProjectToType<OrganizationDto>()
        .ToListAsync(cancellationToken);
    }
}
