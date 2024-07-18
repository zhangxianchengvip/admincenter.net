using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Domain.Constants;
using AdminCenter.Domain.Exceptions;

namespace AdminCenter.Application;

/// <summary>
/// 删除用户
/// </summary>
/// <param name="Id"></param>
public record UserDeleteCommand(Guid Id) : IRequest<bool>;


public class UserDeleteHandler(IApplicationDbContext context, IUser user) : IRequestHandler<UserDeleteCommand, bool>
{
    public async Task<bool> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        if (!request.Id.Equals(user.Id))
        {
            var entity = await context.Users.FindAsync(request.Id, cancellationToken);

            if (entity is not null) context.Users.Remove(entity);

            return true;
        }

        throw new AdminBusinessException(ExctptionMessage.UserDeleteError);
    }
}
