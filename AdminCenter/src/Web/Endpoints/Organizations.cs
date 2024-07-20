
using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Features.Organizations.Commands;
using AdminCenter.Application.Features.Organizations.Dto;
using AdminCenter.Application.Features.Organizations.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AdminCenter.Web.Endpoints;

public class Organizations : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .RequireAuthorization()
           .AddEndpointFilter<ApiResponseFilter>()
           .MapPost(OrganizationCreate)
           .MapGet(OrganizationQuery, "{id}")
           .MapPut(OrganizationUpdate, "{id}")
           .MapDelete(OrganizationDelete, "{id}")
           .MapGet(OrganizationListBySuperiorIdQuery, "{superiorId}");
    }

    /// <summary>
    /// 组织查询
    /// </summary>
    public async Task<OrganizationDto> OrganizationQuery(ISender sender, Guid id)
    {
        return await sender.Send(new OrganizationQuery(id));
    }

    /// <summary>
    /// 组织列表查询
    /// </summary>
    public async Task<List<OrganizationDto>> OrganizationListBySuperiorIdQuery(ISender sender, Guid superiorId)
    {
        return await sender.Send(new OrganizationListBySuperiorIdQuery(superiorId));
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
