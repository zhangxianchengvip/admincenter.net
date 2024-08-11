namespace AdminCenter.Application.Features.Organizations.Commands;

/// <summary>
/// 组织删除
/// </summary>
public record OrganizationDeleteCommand(Guid Id) : IRequest<bool>;

public class OrganizationDeleteHandler(IApplicationDbContext context) : IRequestHandler<OrganizationDeleteCommand, bool>
{
    public async Task<bool> Handle(OrganizationDeleteCommand request, CancellationToken cancellationToken)
    {
        var organization = await context.Organizations
            .Include(s => s.UserOrganizations.Take(1))
            .FirstOrDefaultAsync(o => o.Id.Equals(request.Id));

        if (organization != null && organization.UserOrganizations.Count == 0)
        {
            context.Organizations.Remove(organization);
            return true;
        }

        return organization == null ? true : throw new BusinessException(ExceptionMessage.OrganizationOccupy);
    }
}


