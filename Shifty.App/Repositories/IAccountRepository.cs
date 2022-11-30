using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.ShiftPlanningV1;

namespace Shifty.App.Repositories
{
    public interface IAccountRepository
    {
        Task<Either<Error, EmployeeLoginResponse>> Login(EmployeeLoginDTO loginDto);
    }
}