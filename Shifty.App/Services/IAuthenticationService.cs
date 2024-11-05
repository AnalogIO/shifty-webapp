using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Shifty.App.Services
{
    public interface IAuthenticationService
    {
        Task<bool> LoginUser(string username, string password);
        Task Logout();
        [AllowAnonymous]
        Task<bool> Authenticate(string token);
        Task<bool> Refresh();
    }
}