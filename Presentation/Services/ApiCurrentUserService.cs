using System.Security.Claims;
using Application.Services.Identity;
using IdentityModel;

namespace wms.Services;

public class ApiCurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiCurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId
    {
        get
        {
            var id = _httpContextAccessor.HttpContext?.User.FindFirstValue(JwtClaimTypes.Id);
            return id == null ? null : int.Parse(id);
        }
    }
}