using Blazored.LocalStorage;
using EndavaTechCourseBankApp.Client;
using EndavaTechCourseBankApp.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace EndavaTechCourseBankApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthService>());
            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}