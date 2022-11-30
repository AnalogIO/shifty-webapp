using System.Threading.Tasks;
using Shifty.Api.Generated.ShiftPlanningV1;

namespace Shifty.App.Services
{
    public interface IAuthenticationService
    {
        Task<bool> LoginUser(EmployeeLoginDTO loginDto);
    }
}