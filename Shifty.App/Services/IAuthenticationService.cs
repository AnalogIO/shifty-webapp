using System.Threading.Tasks;

namespace Shifty.App.Services
{
    public interface IAuthenticationService
    {
        Task<bool> LoginUser(string username, string password);
    }
}