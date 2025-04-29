using System.Threading.Tasks;
using Blazored.LocalStorage;
using Shifty.App.Authentication;
using Shifty.App.Repositories;
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
        
        public async Task<bool> LoginUser(string username)
        {
            var either = await _accountRepository.LoginAsync(username);

            return either.Match(
                Left: error =>
                {
                    return false;
                },
                Right: _ => true
                );
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
            var either = await _accountRepository.AuthenticateAsync(token);

            return await either.Match(
                Left: e =>
                {
                    return Task.FromResult(false);
                },
                Right: async response =>
                {
                    var jwtString = response.Jwt;
                    await _localStorage.SetItemAsync("refreshToken", response.RefreshToken);
                    await _localStorage.SetItemAsync("token", jwtString);
                    _authStateProvider.UpdateAuthState(jwtString);

                    return true;
                });
        }
    }
}