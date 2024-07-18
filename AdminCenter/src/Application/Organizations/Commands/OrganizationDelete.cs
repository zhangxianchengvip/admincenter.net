using AdminCenter.Application.Common.Interfaces;

namespace AdminCenter.Application;

public record OrganizationDeleteCommand(Guid Id) : IRequest<bool>;

public class OrganizationDeleteHandler(IApplicationDbContext context) : IRequestHandler<OrganizationDeleteCommand, bool>
{
    public async Task<bool> Handle(OrganizationDeleteCommand request, CancellationToken cancellationToken)
    {
        var organization = await context.Organizations.FindAsync(request.Id, cancellationToken);

        if (organization != null) context.Organizations.Remove(organization);

        return true;
    }
}


