using AdminCenter.Domain.Common.Repository;
using AdminCenter.Domain.DomainServices;

namespace AdminCenter.Application.Features.Roles.Commands;

/// <summary>
/// 角色修改
/// </summary>
public record RoleUpdateCommand(Guid Id, string Name, string ShowName, int Order, string? Description) : IRequest<bool>;

public class RoleUpdateCommandValidator : AbstractValidator<RoleCreateCommand>
{
    public RoleUpdateCommandValidator()
    {
        RuleFor(v => v.Name).NotNull();
    }
}

public class RoleUpdataHandler(IApplicationDbContext context, RoleManager manager) : IRequestHandler<RoleUpdateCommand, bool>
{
    public async Task<bool> Handle(RoleUpdateCommand request, CancellationToken cancellationToken)
    {
        var role = await context.Roles.FindAsync(request.Id);

        if (role != null)
        {
            await manager.UpdateAsync
            (
                role,
                request.Name,
                request.ShowName,
                request.Order,
                request.Description
            );

            return true;
        }

        throw new BusinessException(ExceptionMessage.RoleNotExist);
    }
}
