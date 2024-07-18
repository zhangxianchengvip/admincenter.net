using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Domain.Constants;
using AdminCenter.Domain.Exceptions;
using Mapster;

namespace AdminCenter.Application;

public record OrganizationQuery(Guid Id) : IRequest<OrganizationDto>;

public class OrganizationQueryHandler(IApplicationDbContext context) : IRequestHandler<OrganizationQuery, OrganizationDto>
{
    public async Task<OrganizationDto> Handle(OrganizationQuery request, CancellationToken cancellationToken)
    {
        var organization = await context.Organizations.FindAsync(request.Id);

        if (organization != null) return organization.Adapt<OrganizationDto>();

        throw new AdminBusinessException(ExctptionMessage.OrganizationNotExist);
    }
}
