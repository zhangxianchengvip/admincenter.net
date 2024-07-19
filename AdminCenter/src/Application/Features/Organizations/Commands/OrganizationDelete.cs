namespace AdminCenter.Application.Features.Organizations.Commands;

/// <summary>
/// 组织删除
/// </summary>
public record OrganizationDeleteCommand(Guid Id) : IRequest<bool>;

public class OrganizationDeleteHandler(IApplicationDbContext context) : IRequestHandler<OrganizationDeleteCommand, bool>
{
    public async Task<bool> Handle(OrganizationDeleteCommand request, CancellationToken cancellationToken)
    {
        //TODO:此处需要判断没有在使用
        var organization = await context.Organizations.FindAsync(request.Id, cancellationToken);

        if (organization != null) context.Organizations.Remove(organization);

        return true;
    }
}


