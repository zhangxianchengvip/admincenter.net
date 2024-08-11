
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
#if !DEBUG
           .RequireAuthorization()
#endif
           .AddEndpointFilter<ApiResponseFilter>()
           .MapPost(UserCreate)
           .MapGet(UserListQuery)
           .MapGet(UserQuery, "{id}")
           .MapPut(UserUpdate, "{id}")
           .MapDelete(UserDelete, "{id}")
           .MapGet(PersonalQuery, "/Personal");

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
