using System.Threading.Tasks;
using Blazored.LocalStorage;
using LanguageExt.UnsafeValueAccess;
using Shifty.Api.Generated.ShiftPlanningV1;
using Shifty.App.Authentication;
using Shifty.App.Repositories;

namespace Shifty.App.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IAccountRepository _accountRepository;
        private readonly CustomAuthStateProvider _authStateProvider;
        
        public AuthenticationService(IAccountRepository accountRepository, CustomAuthStateProvider stateProvider, ILocalStorageService storageService)
        {
            _accountRepository = accountRepository;
            _authStateProvider = stateProvider;
            _localStorage = storageService;
        }

        public async Task<bool> LoginUser(EmployeeLoginDTO loginDto)
        {
            var either = await _accountRepository.Login(loginDto);

            if (either.IsLeft)
            {
                System.Console.WriteLine(either.Right(w => w.ToString()).Left(e => e.Message));
                return false;
            }

            var jwtString = either.ValueUnsafe().Token;
            await _localStorage.SetItemAsync("token", jwtString);
            return _authStateProvider.UpdateAuthState(jwtString);
        }
    }
}