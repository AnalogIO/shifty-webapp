using System.Threading.Tasks;

namespace Shifty.App.Services
{
    public interface IAuthenticationService
    {
        Task<bool> LoginUser(string username);
        Task Logout();
        Task<bool> Authenticate(string token);
        Task<bool> Refresh();
    }
}