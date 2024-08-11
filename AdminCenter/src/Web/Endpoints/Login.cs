using System.Security.Claims;
using AdminCenter.Application.Features.Users.Queries;


namespace AdminCenter.Web;
public class Login : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .AddEndpointFilter<ApiResponseFilter>()
           .MapPost(AccountLogin, "Account");

    }

    /// <summary>
    /// 登录
    /// </summary>
    public async Task<object> AccountLogin(ISender sender, TokenBuilder tokenBuilder, UserLogin query)
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
}
