
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdminCenter.Application;
using AdminCenter.Application.Common.Interfaces;
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
              .MapGet(UserInfoQuery, "{id}")
              .MapGet(PersonalInfoQuery, "/Personal");
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="options"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<UserDto> UserLogin(
        ISender sender,
        [FromServices] IOptionsSnapshot<JwtOptions> options,
        UserLoginQuery query)
    {
        var userDto = await sender.Send(query);

        var jwtOptions = options.Value;

        var claims = new[]
               {
            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()!),
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
    public async Task<UserDto> UserInfoQuery(ISender sender, Guid id)
    {
        return await sender.Send(new UserInfoQuery(id));
    }

    /// <summary>
    /// 个人信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<UserDto> PersonalInfoQuery(ISender sender, IUser user)
    {
        return await sender.Send(new UserInfoQuery(Guid.Parse(user.Id!)));
    }
}
