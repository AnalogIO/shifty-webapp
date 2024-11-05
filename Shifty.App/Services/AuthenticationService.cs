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

            return either.Match(
                Left: error =>
                {
                    Console.WriteLine(error);
                    return false;
                },
                Right: _ => true
                );

            // var jwtString = either.ValueUnsafe().Token;
            // await _localStorage.SetItemAsync("token", jwtString);
            // return _authStateProvider.UpdateAuthState(jwtString);
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            await _localStorage.RemoveItemAsync("refreshToken");
            _authStateProvider.UpdateAuthState("");
        }

        [AllowAnonymous]
        public async Task<bool> Refresh()
        {
            var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
            return await Authenticate(refreshToken);
        }

        [AllowAnonymous]
        public async Task<bool> Authenticate(string token)
        {
            Console.WriteLine("Refreshing token");
            Console.WriteLine(token);

            var either = await _accountRepository.AuthenticateAsync(token);

            return await either.Match(
                Left: e =>
                {
                    Console.WriteLine(e.Message);
                    return Task.FromResult(false);
                },
                Right: async response =>
                {
                    Console.WriteLine("Refreshing token successful");

                    var jwtString = response.Jwt;
                    await _localStorage.SetItemAsync("refreshToken", response.RefreshToken);
                    await _localStorage.SetItemAsync("token", jwtString);
                    _authStateProvider.UpdateAuthState(jwtString);

                    return true;
                });
        }
    }
}