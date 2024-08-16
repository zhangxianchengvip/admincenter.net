
using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Features.Menus.Commands;
using AdminCenter.Application.Features.Menus.Dtos;
using AdminCenter.Application.Features.Menus.Queries;

namespace AdminCenter.Web.Endpoints;

public class Menus : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
#if !DEBUG
           .RequireAuthorization()
#endif
           .AddEndpointFilter<ApiResponseFilter>()
           .MapPost(MenuCreate)
           .MapGet(MenuQuery, "{id}")
           .MapPut(MenuUpdate, "{id}")
           .MapDelete(MenuDelete, "{id}")
           .MapGet(MenuListBySuperiorIdQuery, "{superiorId}")
           .MapGet(MenuListWithChildrenQuery, "WithChildren")
           .MapGet(MenuListWithPaginationAndChildrenQuery, "WithPaginationAndChildren");
    }

    /// <summary>
    /// 菜单查询
    /// </summary>
    public async Task<MenuDto> MenuQuery(ISender sender, Guid id)
    {
        return await sender.Send(new MenuQuery(id));
    }

    /// <summary>
    /// 菜单列表查询
    /// </summary>
    public async Task<List<MenuDto>> MenuListBySuperiorIdQuery(ISender sender, Guid superiorId)
    {
        return await sender.Send(new MenuListBySuperiorIdQuery(superiorId));
    }


    /// <summary>
    /// 菜单列表查询
    /// </summary>
    public async Task<List<MenuWithChildrenDto>> MenuListWithChildrenQuery(ISender sender)
    {
        return await sender.Send(new MenuListWithChildrenQuery());
    }


    /// <summary>
    /// 菜单列表查询
    /// </summary>
    public async Task<PaginatedList<MenuWithChildrenDto>> MenuListWithPaginationAndChildrenQuery(ISender sender, [AsParameters] MenuListWithPaginationAndChildrenQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// 组织创建
    /// </summary>
    public async Task<bool> MenuCreate(ISender sender, MenuCreateCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// 菜单修改
    /// </summary>
    public async Task<MenuDto> MenuUpdate(ISender sender, Guid id, MenuUpdateCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// 组织删除
    /// </summary>
    public async Task<bool> MenuDelete(ISender sender, Guid id)
    {
        return await sender.Send(new MenuDeleteCommand(id));
    }
}
