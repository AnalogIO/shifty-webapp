using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Shifty.Api.Generated.ShiftPlanningV1;
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

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.RequireInteraction = true;
            });
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();

            
            services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("ShiftPlanning"));
            services.AddHttpClient("ShiftPlanning",
                    client => client.BaseAddress = new Uri("https://analogio.dk/shiftplanning/"))
                .AddHttpMessageHandler<RequestAuthenticationHandler>();
            services.AddScoped(provider => 
                new ShiftPlanningV1(provider.GetRequiredService<IHttpClientFactory>().CreateClient("ShiftPlanning")));
            services.AddScoped<IShiftRepository, ShiftRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<CustomAuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetService<CustomAuthStateProvider>());
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<RequestAuthenticationHandler>();
            
        }
    }
}