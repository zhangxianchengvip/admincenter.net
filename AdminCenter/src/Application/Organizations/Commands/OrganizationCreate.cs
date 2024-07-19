using AdminCenter.Domain;

namespace AdminCenter.Application;

/// <summary>
/// 创建
/// </summary>
public record OrganizationCreateCommand(
    string Name,
    string Code,
    string? Description = null,
    Guid? superiorOrganizationId = null) : IRequest<bool>;

public class OrganizationCreateCommandValidator : AbstractValidator<OrganizationCreateCommand>
{
    public OrganizationCreateCommandValidator()
    {
        RuleFor(v => v.Name).NotNull();
        RuleFor(v => v.Code).NotNull();
    }
}
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

