namespace AdminCenter.Application.Features.Roles.Commands;

/// <summary>
/// 角色删除
/// </summary>
public record RoleDeleteCommand(Guid Id) : IRequest<bool>;

public class RoleDeleteHandler(IApplicationDbContext context) : IRequestHandler<RoleDeleteCommand, bool>
{
    public async Task<bool> Handle(RoleDeleteCommand request, CancellationToken cancellationToken)
    {
        var role = await context.Roles
            .Include(s => s.UserRoles.Take(1))
            .FirstOrDefaultAsync(s => s.Id.Equals(request.Id));

        if (role != null && role.UserRoles.Count == 0)
        {
            context.Roles.Remove(role);
            return true;
        }

        return role == null ? true : throw new BusinessException(ExceptionMessage.RoleOccupy);
    }
}


