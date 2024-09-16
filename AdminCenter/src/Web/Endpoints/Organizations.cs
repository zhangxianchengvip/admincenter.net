
using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Features.Organizations.Commands;
using AdminCenter.Application.Features.Organizations.Dto;
using AdminCenter.Application.Features.Organizations.Dtos;
using AdminCenter.Application.Features.Organizations.Queries;

namespace AdminCenter.Web.Endpoints;

public class Organizations : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
#if !DEBUG
           .RequireAuthorization()
#endif
           .AddEndpointFilter<ApiResponseFilter>()
           .MapPost(OrganizationCreate)
           .MapGet(OrganizationQuery, "{id}")
           .MapPut(OrganizationUpdate, "{id}")
           .MapDelete(OrganizationDelete, "{id}")
           .MapGet(OrganizationListBySuperiorIdQuery)
           .MapGet(OrganizationListWithChildrenQuery, "WithChildren")
           .MapGet(OrganizationListWithPaginationAndChildrenQuery, "WithPaginationAndChildren")
           .MapGet(OrganizationListWithPaginationBySuperiorIdQuery, "WithPaginationBySuperiorId");
    }

    /// <summary>
    /// 组织查询
    /// </summary>
    public async Task<OrganizationDto> OrganizationQuery(ISender sender, Guid id)
    {
        return await sender.Send(new OrganizationQuery(id));
    }


    /// <summary>
    /// 组织列表及其下级查询
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public async Task<List<OrganizationWithChildrenDto>> OrganizationListWithChildrenQuery(ISender sender)
    {
        return await sender.Send(new OrganizationListWithChildrenQuery());
    }
    /// <summary>
    /// 分页组织列表及其下级查询
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public async Task<PaginatedList<OrganizationWithChildrenDto>> OrganizationListWithPaginationAndChildrenQuery(ISender sender, [AsParameters] OrganizationListWithPaginationAndChildrenQuery query)
    {
        return await sender.Send(query);
    }
    /// <summary>
    /// 组织列表查询
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public async Task<List<OrganizationDto>> OrganizationListBySuperiorIdQuery(ISender sender, [AsParameters] OrganizationListBySuperiorIdQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// 组织列表查询
    /// </summary>
    public async Task<PaginatedList<OrganizationDto>> OrganizationListWithPaginationBySuperiorIdQuery(ISender sender, [AsParameters] OrganizationListWithPaginationBySuperiorIdQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// 组织创建
    /// </summary>
    public async Task<bool> OrganizationCreate(ISender sender, OrganizationCreateCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// 组织修改
    /// </summary>
    public async Task<OrganizationDto> OrganizationUpdate(ISender sender, Guid id, OrganizationUpdateCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// 组织删除
    /// </summary>
    public async Task<bool> OrganizationDelete(ISender sender, Guid id)
    {
        return await sender.Send(new OrganizationDeleteCommand(id));
    }

}
