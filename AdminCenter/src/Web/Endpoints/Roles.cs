
using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Roles.Dto;
using AdminCenter.Application.Roles.Queries;
using AdminCenter.Application.Users.Dto;

namespace AdminCenter.Web.Endpoints;

public class Roles : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
              .MapGet(RoleQuery, "{Id}")
              .MapGet(RoleListQuery);
    }

    public async Task<RoleDto> RoleQuery(ISender sender, Guid Id)
    {
        return await sender.Send(new RoleQuery(Id));
    }
    public async Task<PaginatedList<RoleDto>> RoleListQuery(ISender sender, [AsParameters] RoleListQuery query)
    {
        return await sender.Send(query);
    }
}
