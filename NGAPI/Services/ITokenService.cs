using NGAPI.Models;

namespace NGAPI.Services
{
    public interface ITokenService
    {
        string GetToken(AppUser user);
    }
}
