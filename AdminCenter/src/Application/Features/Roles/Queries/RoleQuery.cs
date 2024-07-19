using AdminCenter.Application.Features.Roles.Dto;

namespace AdminCenter.Application.Features.Roles.Queries;

/// <summary>
/// 角色
/// </summary>
public record RoleQuery(Guid Id) : IRequest<RoleDto>;

public class RoleQueryHandler(IApplicationDbContext context) : IRequestHandler<RoleQuery, RoleDto>
{
    public async Task<RoleDto> Handle(RoleQuery request, CancellationToken cancellationToken)
    {
        var role = await context.Roles.FindAsync(request.Id);

        return role != null ? role.Adapt<RoleDto>() : throw new BusinessException(ExceptionMessage.RoleNotExist);
    }
}
