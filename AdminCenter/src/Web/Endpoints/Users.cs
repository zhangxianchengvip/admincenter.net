
using AdminCenter.Application;
using AdminCenter.Application.Common.Security;
using AdminCenter.Application.Users.Dto;
using AdminCenter.Application.Users.Queries;

namespace AdminCenter.Web;
[Authorize]
public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .MapPost(UserLogin, "Login");

        app.MapGroup(this)
           .RequireAuthorization()
           .MapGet(UserInfoQuery, "{id}");
    }

    public async Task<UserDto> UserLogin(ISender sender, UserLoginQuery query)
    {
        var userDto = await sender.Send(query);

        return userDto;
    }

    public async Task<UserDto> UserInfoQuery(ISender sender, Guid id)
    {
        var userDto = await sender.Send(new UserInfoQuery(id));

        return userDto;
    }
}
