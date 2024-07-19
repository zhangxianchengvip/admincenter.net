
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
           .MapPost(RoleCreate)
           .MapGet(RoleListQuery)
           .MapGet(RoleQuery, "{Id}")
           .MapPut(RoleUpdate, "{Id}")
           .MapDelete(RoleDelete, "{Id}");
    }

    public async Task<RoleDto> RoleQuery(ISender sender, Guid Id)
    {
        return await sender.Send(new RoleQuery(Id));
    }
    public async Task<PaginatedList<RoleDto>> RoleListQuery(ISender sender, [AsParameters] RoleListQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<RoleDto> RoleCreate(ISender sender, RoleCreateCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<bool> RoleUpdate(ISender sender, Guid Id, RoleUpdateCommand command)
    {
        return await sender.Send(command);
    }
    public async Task<bool> RoleDelete(ISender sender, Guid Id)
    {
        return await sender.Send(new RoleDeleteCommand(Id));
    }
}
