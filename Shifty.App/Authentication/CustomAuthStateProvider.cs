using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Shifty.Api.Generated.AnalogCoreV2;

namespace Shifty.App.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly AnalogCoreV2 _client;

        public CustomAuthStateProvider(ILocalStorageService storageService, AnalogCoreV2 client)
        {
            _localStorage = storageService;
            _client = client;
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
            Console.WriteLine("Authenticating with token: " + jwtString);
            if (!tokenHandler.CanReadToken(jwtString))
            {
                Console.WriteLine("Cannot read token");
                return new ClaimsPrincipal();
            }

            var token = new JwtSecurityTokenHandler().ReadJwtToken(jwtString);
            return (DateTime.UtcNow < token.ValidTo) switch
            {
                true => new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "bearerToken")), //Needs the string passed as well, otherwise the user is not set to authenticated
                false => new ClaimsPrincipal()
            };
            // switch(DateTime.UtcNow < token.ValidTo)
            // {
            //     case true:
            //         return new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "bearerToken")); //Needs the string passed as well, otherwise the user is not set to authenticated
            //     case false:
            //         var t = await _client.ApiV2AccountAuthRefreshAsync(LoginType.Shifty, null);
            //         if (t is not null)
            //         {
            //             await _localStorage.SetItemAsync("token", t.Jwt);
            //             return new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "bearerToken"));
            //         } else
            //         {
            //             return new ClaimsPrincipal();
            //         }
            // }
        }
    }
}