using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace api.Helpers;

public class UserResolverService
{
    private readonly IHttpContextAccessor _context;

    public UserResolverService(IHttpContextAccessor context)
    {
        this._context = context;
    }

    public string GetUser()
    {
        try
        {
            var claims = _context.HttpContext?.User?.Claims;

            if (claims?.Any() == true)
            {
                var nameId = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (nameId != null)
                {
                    return nameId.Value;
                }
            }

            return _context.HttpContext.User.Identity.Name;
        }
        catch (Exception)
        {
        }

        return string.Empty;
    }
}
