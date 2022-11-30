using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace Shifty.App.Authentication
{
    public class RequestAuthenticationHandler : DelegatingHandler 
    {
        private readonly ILocalStorageService _localStorage;

        public RequestAuthenticationHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, 
            CancellationToken cancellationToken)
        {
            // Add custom functionality here, before or after base.SendAsync()
            var jwtString = await _localStorage.GetItemAsync<string>("token");
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", jwtString);
            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}