namespace AdminCenter.Application.Roles.Commands;
public record RoleUpdateCommand(Guid Id, string Name, string? Description) : IRequest<bool>;

public class RoleUpdataHandler(IApplicationDbContext context) : IRequestHandler<RoleUpdateCommand, bool>
{
    public async Task<bool> Handle(RoleUpdateCommand request, CancellationToken cancellationToken)
    {
        var role = await context.Roles.FindAsync(request.Id);

        if (role != null)
        {
            role.Description = request.Description;
            role.UpdateRoleName(request.Name);
            return true;
        }

        throw new AdminBusinessException(ExceptionMessage.RoleNotExist);
    }
}
