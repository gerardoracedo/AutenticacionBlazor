using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazored.Modal;
using System.Reflection;
using System.Globalization;
using AKSoftware.Localization.MultiLanguages;
using Microsoft.AspNetCore.Identity;
using Tewr.Blazor.FileReader;
using AutenticacionBlazor.Shared.Servicios;
using Blazored.LocalStorage;
using MudBlazor.Services;

namespace AutenticacionBlazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddLanguageContainer(Assembly.GetExecutingAssembly(), CultureInfo.GetCultureInfo("es-AR"));
            builder.Services.AddHttpClient("AutenticacionBlazor.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddBlazoredLocalStorage();
            //builder.Services.AddScoped<DialogService>();
            //builder.Services.AddScoped<NotificationService>();
            //builder.Services.AddScoped<TooltipService>();
            //builder.Services.AddScoped<ContextMenuService>();
            builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);

            builder.Services.AddMudServices();

            ConfigureServices(builder.Services);
            await builder.Build().RunAsync();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            // Supply HttpClient instances that include access tokens when making requests to the server project

            services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("AutenticacionBlazor.ServerAPI"));
            services.AddFileReaderService(options => options.InitializeOnFirstCall = true);
            services.AddApiAuthorization().AddAccountClaimsPrincipalFactory<RolesClaimsPrincipalFactory>();
            services.AddBlazoredModal();
            //services.AddScoped<UsuarioServicio>();
        }
    }
}
