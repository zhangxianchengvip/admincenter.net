using AdminCenter.Domain;

namespace AdminCenter.Application;

/// <summary>
/// 组织创建
/// </summary>
public record OrganizationUpdateCommand(
    Guid Id,
    string Name,
    string Code,
    string? Description,
    Guid? SuperiorOrganizationId,
    StatusEnum Status) : IRequest<OrganizationDto>;

public class OrganizationUpdateCommandValidator : AbstractValidator<OrganizationUpdateCommand>
{
    public OrganizationUpdateCommandValidator()
    {
        RuleFor(v => v.Id).NotNull();
        RuleFor(v => v.Name).NotNull();
        RuleFor(v => v.Code).NotNull();
    }
}
public class UpdateOrganizationHandler(IApplicationDbContext context, OrganizationManager manager) : IRequestHandler<OrganizationUpdateCommand, OrganizationDto>
{
    public async Task<OrganizationDto> Handle(OrganizationUpdateCommand request, CancellationToken cancellationToken)
    {
        var organization = await context.Organizations.FindAsync(request.id);

        if (organization != null)
        {
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

        throw new BusinessException(ExceptionMessage.OrganizationNotExist);
    }
}

