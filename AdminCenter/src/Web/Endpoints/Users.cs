
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdminCenter.Application;
using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Common.Security;
using AdminCenter.Application.Users.Dto;
using AdminCenter.Application.Users.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AdminCenter.Web;
[Authorize]
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
           .MapPut(UserUpdate, "{id}")
           .MapDelete(UserDelete, "{id}")
           .MapGet(UserQuery, "{id}")
           .MapGet(PersonalQuery, "/Personal");

    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="options"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<UserDto> UserLogin(ISender sender, IOptionsSnapshot<JwtOptions> options, UserLogin query)
    {
        var userDto = await sender.Send(query);

        var jwtOptions = options.Value;

        //1. 创建 claims
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            new Claim(ClaimTypes.Name, userDto.LoginName!), //HttpContext.User.Identity.Name
            new Claim(ClaimTypes.Role, "admin"), //HttpContext.User.IsInRole("r_admin")
            new Claim("Username",userDto.RealName??""),
        };

        // 2. 从 appsettings.json 中读取SecretKey
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

        // 3. 选择加密算法
        var algorithm = SecurityAlgorithms.HmacSha256;

        // 4. 生成Credentials
        var signingCredentials = new SigningCredentials(secretKey, algorithm);

        // 5. 根据以上，生成token
        var jwtSecurityToken = new JwtSecurityToken
        (
            jwtOptions.Issuer,     //Issuer
            jwtOptions.Audience,   //Audience
            claims,                          //Claims,
            DateTime.Now,                    //notBefore
            DateTime.Now.AddSeconds(jwtOptions.Expires),    //expires
            signingCredentials               //Credentials
        );

        // 6. 将token变为string
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        userDto.Token = token;

        return userDto;
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="id"></param>
    /// <returns></returns>
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
    /// <param name="sender"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<UserDto> PersonalQuery(ISender sender, IUser user)
    {
        return await sender.Send(new UserQuery(Guid.Parse(user.Id!)));
    }

    /// <summary>
    /// 用户创建
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<UserDto> UserCreate(ISender sender, UserCreateCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// 用户更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<UserDto> UserUpdate(ISender sender, Guid id, UserUpdateCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// 用户删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> UserDelete(ISender sender, Guid id)
    {
        return await sender.Send(new UserDeleteCommand(id));
    }
}
