namespace AdminCenter.Application.Roles.Commands;
public record RoleDeleteCommand(Guid Id) : IRequest<bool>;


public class RoleDeleteHandler(IApplicationDbContext context) : IRequestHandler<RoleDeleteCommand, bool>
{
    public async Task<bool> Handle(RoleDeleteCommand request, CancellationToken cancellationToken)
    {
        //TODO:此处需要确定没有在使用的人和菜单

        var role = await context.Roles.FindAsync(request.Id);

        if (role != null) context.Roles.Remove(role);

        return true;
    }
}


