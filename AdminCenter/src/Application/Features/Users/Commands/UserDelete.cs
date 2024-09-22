using AdminCenter.Domain.Common.Repository;

namespace AdminCenter.Application.Features.Users.Commands;

/// <summary>
/// 删除用户
/// </summary>
public record UserDeleteCommand(Guid Id) : IRequest<bool>;


public class UserDeleteHandler(IApplicationDbContext context, IUser<Guid> user) : IRequestHandler<UserDeleteCommand, bool>
{
    public async Task<bool> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        if (!request.Id.Equals(user.Id))
        {
            var entity = await context.Users.FindAsync(request.Id, cancellationToken);

            if (entity is not null) context.Users.Remove(entity);

            return true;
        }

        throw new BusinessException(ExceptionMessage.UserDeleteError);
    }
}
