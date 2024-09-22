using System;
using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Features.Menus.Dtos;
using AdminCenter.Application.Features.Menus.Extensions;
using AdminCenter.Domain.Common.Repository;

namespace AdminCenter.Application.Features.Menus.Queries;

public record class MenuListWithPaginationAndChildrenQuery(int PageNumber, int PageSize) : IRequest<PaginatedList<MenuWithChildrenDto>>;

public class MenuListWithPaginationAndChildrenQueryHandler(IApplicationDbContext context) : IRequestHandler<MenuListWithPaginationAndChildrenQuery, PaginatedList<MenuWithChildrenDto>>
{
    public async Task<PaginatedList<MenuWithChildrenDto>> Handle(MenuListWithPaginationAndChildrenQuery request, CancellationToken cancellationToken)
    {
        var menuList = await context.Menus
        .ToListAsync();

        var result = menuList
        .BuildMenuTree()
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize)
        .ToList();

        return new PaginatedList<MenuWithChildrenDto>(result, result.Count, request.PageNumber, request.PageSize);
    }
}