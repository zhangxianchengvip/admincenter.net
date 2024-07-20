using AdminCenter.Application.Features.Organizations.Dto;

namespace AdminCenter.Application.Features.Organizations.Queries;

/// <summary>
/// 组织查询
/// </summary>
public record OrganizationQuery(Guid Id) : IRequest<OrganizationDto>;

public class OrganizationQueryHandler(IApplicationDbContext context) : IRequestHandler<OrganizationQuery, OrganizationDto>
{
    public async Task<OrganizationDto> Handle(OrganizationQuery request, CancellationToken cancellationToken)
    {
        var organization = await context.Organizations.FindAsync(request.Id);

        return organization != null ? organization.Adapt<OrganizationDto>() : throw new BusinessException(ExceptionMessage.OrganizationNotExist);
    }
}
