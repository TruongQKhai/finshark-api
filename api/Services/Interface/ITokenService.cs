using api.Models;

namespace api.Services.Interface;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
