using System.Threading.Tasks;
using System;
using System.Security.Cryptography;
using System.Text;
using Blazored.LocalStorage;
using LanguageExt.UnsafeValueAccess;
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

        private static string EncodePasscode(string passcode)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(passcode);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passcodeHash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(passcodeHash);
            }
        }
        
        public async Task<bool> LoginUser(string username, string password)
        {
            var encodedPassword = EncodePasscode(password);
            var either = await _accountRepository.LoginAsync(username, encodedPassword);

            if (either.IsLeft)
            {
                System.Console.WriteLine(either.Right(w => w.ToString()).Left(e => e.Message));
                return false;
            }

            var jwtString = either.ValueUnsafe().Token;
            await _localStorage.SetItemAsync("token", jwtString);
            return _authStateProvider.UpdateAuthState(jwtString);
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            _authStateProvider.UpdateAuthState("");
        }
    }
}