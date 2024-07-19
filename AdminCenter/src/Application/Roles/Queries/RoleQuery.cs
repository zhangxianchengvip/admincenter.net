using AdminCenter.Application.Roles.Dto;

namespace AdminCenter.Application.Roles.Queries;

/// <summary>
/// 角色
/// </summary>
public record RoleQuery(Guid Id) : IRequest<RoleDto>;

public class RoleQueryHandler(IApplicationDbContext context) : IRequestHandler<RoleQuery, RoleDto>
{
    public async Task<RoleDto> Handle(RoleQuery request, CancellationToken cancellationToken)
    {
        var role = await context.Roles.FindAsync(request.Id);

        if (role != null) return role.Adapt<RoleDto>();

        throw new BusinessException(ExceptionMessage.RoleNotExist);
    }
}
