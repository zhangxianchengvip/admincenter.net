using AdminCenter.Application.Features.Roles.Dto;

namespace AdminCenter.Application.Features.Roles.Queries;
public record RoleListQuery : IRequest<List<RoleDto>>;

public class RoleListQueryHandler(IApplicationDbContext context) : IRequestHandler<RoleListQuery, List<RoleDto>>
{
    public async Task<List<RoleDto>> Handle(RoleListQuery request, CancellationToken cancellationToken)
    {
        return await context.Roles
        .ProjectToType<RoleDto>()
        .ToListAsync();
    }
}
