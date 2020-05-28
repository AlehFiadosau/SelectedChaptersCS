using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using WebCarInspection.Core;
using WebCarInspection.ViewModels;

namespace WebCarInspection
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<UserViewModel>>();
            var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await DbInitializer.InitializeAsync(userManager, rolesManager);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
