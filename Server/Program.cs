using EndavaTechCourseBankApp.Application.Commands.AddCurrency;
using EndavaTechCourseBankApp.Application.Commands.CreateWallet;
using EndavaTechCourseBankApp.Application.Queries.GetWallets;
using EndavaTechCourseBankApp.Infrastructure;
using EndavaTechCourseBankApp.Server.Composition;
using Microsoft.Extensions.DependencyInjection;


namespace EndavaTechCourseBankApp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddInfrastructure(configuration);
            builder.Services.AddJwtIdentity(configuration);
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
                config.RegisterServicesFromAssemblies(typeof(GetWalletsQuery).Assembly);
                config.RegisterServicesFromAssemblies(typeof(CreateWalletCommand).Assembly);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}