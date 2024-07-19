
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Features.Users.Commands;
using AdminCenter.Application.Features.Users.Dto;
using AdminCenter.Application.Features.Users.Queries;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AdminCenter.Web;
public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .AddEndpointFilter<ApiResponseFilter>()
           .MapPost(UserLogin, "Login");

        app.MapGroup(this)
           .RequireAuthorization()
           .AddEndpointFilter<ApiResponseFilter>()
           .MapPost(UserCreate)
           .MapGet(UserQuery, "{id}")
           .MapPut(UserUpdate, "{id}")
           .MapDelete(UserDelete, "{id}")
           .MapGet(PersonalQuery, "/Personal");

    }

    /// <summary>
    /// 登录
    /// </summary>
    public async Task<object> UserLogin(ISender sender, TokenBuilder tokenBuilder, UserLogin query)
    {
        var userDto = await sender.Send(query);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            new Claim(ClaimTypes.Name, userDto.LoginName!), //HttpContext.User.Identity.Name
            new Claim(ClaimTypes.Role, "admin"), //HttpContext.User.IsInRole("r_admin")
            new Claim("Username",userDto.RealName??""),
        };

        string token = tokenBuilder.Build(claims);

        return new { User = userDto, Token = token };
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public async Task<UserDto> UserQuery(ISender sender, Guid id)
    {
        return await sender.Send(new UserQuery(id));
    }

    /// <summary>
    /// 用户列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<PaginatedList<UserDto>> UserListQuery(ISender sender, int pageNumber, int pageSize)
    {
        return await sender.Send(new UserListQuery(pageNumber, pageSize));
    }
    /// <summary>
    /// 个人信息
    /// </summary>
    public async Task<UserDto> PersonalQuery(ISender sender, IUser<Guid> user)
    {
        return await sender.Send(new UserQuery(user.Id));
    }

    /// <summary>
    /// 用户创建
    /// </summary>
    public async Task<UserDto> UserCreate(ISender sender, UserCreateCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// 用户更新
    /// </summary>
    public async Task<UserDto> UserUpdate(ISender sender, Guid id, UserUpdateCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// 用户删除
    /// </summary>
    public async Task<bool> UserDelete(ISender sender, Guid id)
    {
        return await sender.Send(new UserDeleteCommand(id));
    }
}
