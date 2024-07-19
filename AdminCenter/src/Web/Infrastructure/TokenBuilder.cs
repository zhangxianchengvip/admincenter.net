using AdminCenter.Application.Features.Users.Dto;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace AdminCenter.Web.Infrastructure;

public class TokenBuilder(IOptionsSnapshot<JwtOptions> options)
{
    public string Build(Claim[] claims)
    {
        var jwtOptions = options.Value;

        // 1. 从 appsettings.json 中读取SecretKey
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

        // 2. 选择加密算法
        var algorithm = SecurityAlgorithms.HmacSha256;

        // 3. 生成Credentials
        var signingCredentials = new SigningCredentials(secretKey, algorithm);

        // 4. 根据以上，生成token
        var jwtSecurityToken = new JwtSecurityToken
        (
            jwtOptions.Issuer,     //Issuer
            jwtOptions.Audience,   //Audience
            claims,                          //Claims,
            DateTime.Now,                    //notBefore
            DateTime.Now.AddSeconds(jwtOptions.Expires),    //expires
            signingCredentials               //Credentials
        );

        // 5. 将token变为string
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
