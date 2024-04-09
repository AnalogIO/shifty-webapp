using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using Shifty.Api.Generated.AnalogCoreV1;
using Shifty.Api.Generated.AnalogCoreV2;
using Shifty.App.Authentication;
using Shifty.App.Repositories;
using Shifty.App.Services;
using MudExtensions.Services;

namespace Shifty.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddMudExtensions();
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

            services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("AnalogCoreV2"));
            services.AddHttpClient("AnalogCoreV2",
                    client => client.BaseAddress = new Uri(configuration["ApiHost"]))
                .AddHttpMessageHandler<RequestAuthenticationHandler>();
            services.AddScoped(provider =>
                new AnalogCoreV2(provider.GetRequiredService<IHttpClientFactory>().CreateClient("AnalogCoreV2")));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<CustomAuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetService<CustomAuthStateProvider>());
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IVoucherService, VoucherService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<RequestAuthenticationHandler>();

            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });


        }
    }
}