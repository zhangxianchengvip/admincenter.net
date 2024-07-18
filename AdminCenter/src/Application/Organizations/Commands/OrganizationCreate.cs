
using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Domain;

namespace AdminCenter.Application;

/// <summary>
/// 创建
/// </summary>
/// <param name="Name"></param>
/// <param name="Code"></param>
/// <param name="Description"></param>
/// <param name="superiorOrganizationId"></param>
public record OrganizationCreateCommand(
    string Name,
    string Code,
    string? Description = null,
    Guid? superiorOrganizationId = null) : IRequest<bool>;


public class OrganizationCreateHandler(IApplicationDbContext context, OrganizationManager manager) : IRequestHandler<OrganizationCreateCommand, bool>
{
    public async Task<bool> Handle(OrganizationCreateCommand request, CancellationToken cancellationToken)
    {
        var organization = await manager.CreateAsync
        (
            request.Name,
            request.Code,
            request.Description,
            request.superiorOrganizationId
        );

        await context.Organizations.AddAsync(organization);

        return true;
    }
}

