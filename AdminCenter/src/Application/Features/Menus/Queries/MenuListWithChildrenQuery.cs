using System;
using AdminCenter.Application.Features.Menus.Dtos;
using AdminCenter.Application.Features.Menus.Extensions;
using AdminCenter.Domain.Common.Repository;

namespace AdminCenter.Application.Features.Menus.Queries;

public record class MenuListWithChildrenQuery : IRequest<List<MenuWithChildrenDto>>;

public class MenuListWithChildrenQueryHandler(IApplicationDbContext context) : IRequestHandler<MenuListWithChildrenQuery, List<MenuWithChildrenDto>>
{
    public async Task<List<MenuWithChildrenDto>> Handle(MenuListWithChildrenQuery request, CancellationToken cancellationToken)
    {
        var menuList = await context.Menus
        .ToListAsync();

        return menuList.BuildMenuTree();
    }
}