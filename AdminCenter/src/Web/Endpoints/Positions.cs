using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Features.Positions.Commands;
using AdminCenter.Application.Features.Positions.Dtos;
using AdminCenter.Application.Features.Positions.Queries;
using AdminCenter.Application.Features.Roles.Commands;
using AdminCenter.Application.Features.Roles.Dto;
using AdminCenter.Application.Features.Roles.Queries;

namespace AdminCenter.Web.Endpoints;

public class Positions : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
#if !DEBUG
           .RequireAuthorization()
#endif
           .AddEndpointFilter<ApiResponseFilter>()
           .MapPost(PositionCreate)
           .MapGet(PositionListQuery)
           .MapGet(PositionQuery, "{id}")
           .MapPut(PositionUpdate, "{id}")
           .MapDelete(PositionDelete, "{id}");
    }

    /// <summary>
    ///  职位查询
    /// </summary>
    public async Task<PositionDto> PositionQuery(ISender sender, Guid id)
    {
        return await sender.Send(new PositionQuery(id));
    }

    /// <summary>
    /// 职位列表
    /// </summary>
    public async Task<PaginatedList<PositionDto>> PositionListQuery(ISender sender, [AsParameters] PositionListQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// 职位创建
    /// </summary>
    public async Task<PositionDto> PositionCreate(ISender sender, PositionCreateCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// 职位修改
    /// </summary>
    public async Task<PositionDto> PositionUpdate(ISender sender, Guid id, PositionUpdateCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// 职位删除
    /// </summary>
    public async Task<bool> PositionDelete(ISender sender, Guid id)
    {
        return await sender.Send(new PositionDeleteCommand(id));
    }
}
