using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using Shifty.Api.Generated.ShiftPlanningV1;
using static LanguageExt.Prelude;

namespace Shifty.App.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ShiftPlanningV1 _client;

        public AccountRepository(ShiftPlanningV1 client)
        {
            _client = client;
        }
        
        public async Task<Either<Error, EmployeeLoginResponse>> Login(EmployeeLoginDTO loginDto)
        {
            return await TryAsync(_client.ApiAccountLoginAsync(loginDto)).ToEither();
        }
    }
}