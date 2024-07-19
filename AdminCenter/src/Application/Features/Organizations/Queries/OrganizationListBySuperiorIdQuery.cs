using AdminCenter.Application.Features.Organizations.Dto;

namespace AdminCenter.Application.Features.Organizations.Queries;

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
        .Where(s => s.SuperiorId.Equals(request.Id))
        .ProjectToType<OrganizationDto>()
        .ToListAsync(cancellationToken);
    }
}
