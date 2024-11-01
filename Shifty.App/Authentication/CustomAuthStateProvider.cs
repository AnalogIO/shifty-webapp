using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Shifty.Api.Generated.AnalogCoreV2;
using Shifty.App.Services;

namespace Shifty.App.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IAuthenticationService _authService;

        public CustomAuthStateProvider(ILocalStorageService storageService, IAuthenticationService authService)
        {
            _localStorage = storageService;
            _authService = authService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jwtString = await _localStorage.GetItemAsync<string>("token");
            var user = await ParseJwtString(jwtString);
            return new AuthenticationState(user);
        }

        [AllowAnonymous]
        public async Task<bool> UpdateAuthState(string jwtString)
        {
            var user = await ParseJwtString(jwtString);
            Console.WriteLine("User authenticated: " + user.Identity.IsAuthenticated);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
            return true;
        }

        private async Task<ClaimsPrincipal> ParseJwtString (string jwtString)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            if (!tokenHandler.CanReadToken(jwtString))
            {
                return new ClaimsPrincipal();
            }

            var token = new JwtSecurityTokenHandler().ReadJwtToken(jwtString);
            switch(DateTime.UtcNow < token.ValidTo)
            {
                case true:
                    return new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "bearerToken")); //Needs the string passed as well, otherwise the user is not set to authenticated
                case false:
                    var t = await _authService.Refresh();
                    if (t)
                    {
                        return new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "bearerToken"));
                    } else
                    {
                        return new ClaimsPrincipal();
                    }
            }
        }
    }
}