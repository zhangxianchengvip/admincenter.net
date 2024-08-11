
using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Features.Roles.Commands;
using AdminCenter.Application.Features.Roles.Dto;
using AdminCenter.Application.Features.Roles.Queries;

namespace AdminCenter.Web.Endpoints;

public class Roles : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
#if !DEBUG
           .RequireAuthorization()
#endif
           .AddEndpointFilter<ApiResponseFilter>()
           .MapPost(RoleCreate)
           .MapGet(RoleListQuery)
           .MapGet(RoleQuery, "{id}")
           .MapPut(RoleUpdate, "{id}")
           .MapDelete(RoleDelete, "{id}")
           .MapGet(RoleListWithPaginationQuery, "WithPagination");
    }

    /// <summary>
    /// 角色查询
    /// </summary>
    public async Task<RoleDto> RoleQuery(ISender sender, Guid id)
    {
        return await sender.Send(new RoleQuery(id));
    }

    public async Task<List<RoleDto>> RoleListQuery(ISender sender)
    {
        return await sender.Send(new RoleListQuery());
    }

    /// <summary>
    /// 角色列表
    /// </summary>
    public async Task<PaginatedList<RoleDto>> RoleListWithPaginationQuery(ISender sender, [AsParameters] RoleListWithPaginationQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// 角色创建
    /// </summary>
    public async Task<RoleDto> RoleCreate(ISender sender, RoleCreateCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// 角色修改
    /// </summary>
    public async Task<bool> RoleUpdate(ISender sender, Guid id, RoleUpdateCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// 角色删除
    /// </summary>
    public async Task<bool> RoleDelete(ISender sender, Guid id)
    {
        return await sender.Send(new RoleDeleteCommand(id));
    }
}
