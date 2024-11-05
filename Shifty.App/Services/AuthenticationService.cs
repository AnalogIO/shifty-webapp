using System.Threading.Tasks;
using System;
using System.Security.Cryptography;
using System.Text;
using Blazored.LocalStorage;
using LanguageExt.UnsafeValueAccess;
using Shifty.App.Authentication;
using Shifty.App.Repositories;
using MudBlazor.Extensions;
using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;

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

            return true; // Email has been sent, allegedly

            // var jwtString = either.ValueUnsafe().Token;
            // await _localStorage.SetItemAsync("token", jwtString);
            // return _authStateProvider.UpdateAuthState(jwtString);
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            _authStateProvider.UpdateAuthState("");
        }

        [AllowAnonymous]
        public async Task Authenticate(string token)
        {
            var res = await _accountRepository.AuthenticateAsync(token);
            await _localStorage.SetItemAsync("token", res.Jwt);
            await _localStorage.SetItemAsync("refreshToken", res.RefreshToken);
            _authStateProvider.UpdateAuthState(res.Jwt);
        }

        public async Task<bool> Refresh()
        {
            Console.WriteLine("Refreshing token");
            var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
            var res = await _accountRepository.RefreshTokenAsync(refreshToken);

            if (res.IsLeft)
            {
                System.Console.WriteLine(res.Right(w => w.ToString()).Left(e => e.Message));
                return false;
            }

            Console.WriteLine("Refreshing token successful");

            var actualRes = res.ValueUnsafe();
            var jwtString = actualRes.Jwt;
            await _localStorage.SetItemAsync("refreshToken", actualRes.RefreshToken);
            await _localStorage.SetItemAsync("token", jwtString);
            _authStateProvider.UpdateAuthState(jwtString);

            return true;
        }
    }
}