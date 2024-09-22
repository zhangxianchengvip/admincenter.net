using AdminCenter.Application.Features.Menus.Dtos;
using AdminCenter.Domain.Common.Repository;

namespace AdminCenter.Application.Features.Menus.Queries;
/// <summary>
/// 获取下级组织
/// </summary>
/// <param name="Id"></param>
public record MenuListBySuperiorIdQuery(Guid? Id) : IRequest<List<MenuDto>>;

public class MenuListBySuperiorIdQueryHandler(IApplicationDbContext context) : IRequestHandler<MenuListBySuperiorIdQuery, List<MenuDto>>
{
    public async Task<List<MenuDto>> Handle(MenuListBySuperiorIdQuery request, CancellationToken cancellationToken)
    {
        return await context.Menus
        .Where(s => s.SuperiorId.Equals(request.Id))
        .ProjectToType<MenuDto>()
        .ToListAsync(cancellationToken);
    }
}

