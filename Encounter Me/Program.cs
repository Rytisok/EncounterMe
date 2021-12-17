using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.JSInterop;
using ExtensionMethods;
using Encounter_Me.Pages;
using Encounter_Me.Services;
using Encounter_Me.Authentication;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorCurrentDevice;
using Serilog;
using Destructurama;

namespace Encounter_Me
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args); ;
            builder.RootComponents.Add<App>("#app");

           
            Log.Logger = new LoggerConfiguration() //Only logs to console for now. There is a possibility to log to server but it is a bit complicated
           .Destructure.UsingAttributes()
           .WriteTo.BrowserConsole()
           .CreateLogger();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Logging.SetMinimumLevel(LogLevel.Warning);///Can set minimal logging level

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped<IUserDataService, UserDataService>();
            builder.Services.AddScoped<ITrailService, TrailService>();
            builder.Services.AddScoped<ICapturePointService, CapturePointService>();
            builder.Services.AddBlazorCurrentDevice();
            await builder.Build().RunAsync();
        }
    }
}
