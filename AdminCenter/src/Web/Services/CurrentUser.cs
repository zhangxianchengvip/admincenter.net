using System.ComponentModel;
using System.Security.Claims;

using AdminCenter.Application.Common.Interfaces;
using YamlDotNet.Core.Tokens;

namespace AdminCenter.Web.Services;

public class CurrentUser<TKey> : IUser<TKey>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public TKey? Id
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return default;

            var converter = TypeDescriptor.GetConverter(typeof(TKey));

            return (TKey)converter.ConvertFromInvariantString(userId)!;
        }
    }
}




