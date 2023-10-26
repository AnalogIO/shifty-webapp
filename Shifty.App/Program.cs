using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.App.Authentication;
using Shifty.App.Repositories;
using Shifty.App.Services;

namespace Shifty.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            ConfigureServices(builder.Services, builder.Configuration);

            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.RequireInteraction = true;
            });
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();

            
            services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("AnalogCoreV1"));
            services.AddHttpClient("AnalogCoreV1",
                    client => client.BaseAddress = new Uri(configuration["ApiHost"]))
                .AddHttpMessageHandler<RequestAuthenticationHandler>();
            services.AddScoped(provider => 
                new AnalogCoreV1(provider.GetRequiredService<IHttpClientFactory>().CreateClient("AnalogCoreV1")));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<CustomAuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetService<CustomAuthStateProvider>());
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<RequestAuthenticationHandler>();
            
        }
    }
}