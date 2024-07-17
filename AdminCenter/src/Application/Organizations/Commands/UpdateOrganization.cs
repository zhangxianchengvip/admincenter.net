using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Domain;
using AdminCenter.Domain.Constants;
using AdminCenter.Domain.Exceptions;
using Mapster;

namespace AdminCenter.Application;

public record UpdateOrganizationCommand(
    Guid id,
    string Name,
    string Code,
    string? Description,
    Guid? SuperiorOrganizationId,
    StatusEnum Status) : IRequest<OrganizationDto>;


public class UpdateOrganizationHandler(
    IApplicationDbContext context,
    OrganizationManager manager) : IRequestHandler<UpdateOrganizationCommand, OrganizationDto>
{
    public async Task<OrganizationDto> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = await context.Organizations.FindAsync(request.id);

        if (organization == null)
        {
            throw new AdminBusinessException(ExctptionMessage.OrganizationNotExist);
        }

        organization = await manager.UpdateAsync
        (
            organization: organization,
            name: request.Name,
            code: request.Code,
            description: request.Description,
            superiorOrganizationId: request.SuperiorOrganizationId
        );

        return organization.Adapt<OrganizationDto>();
    }
}

