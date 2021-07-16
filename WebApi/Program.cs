using System;
using System.Threading.Tasks;
using Infrastructure.Identity.Context;
using Infrastructure.Identity.Models;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Read Configuration from appSettings
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
                var identityContext = services.GetRequiredService<IdentityContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await applicationDbContext.Database.MigrateAsync();
                Log.Information("migrate application database");
                await identityContext.Database.MigrateAsync();
                Log.Information("migrate identity database");

                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.Seq("http://localhost:5341")
                    .WriteTo.File("log.txt")
                    .WriteTo.MSSqlServer(config.GetConnectionString("DefaultConnection"),
                        new MSSqlServerSinkOptions
                        {
                            TableName = "Logs",
                            SchemaName = "school",
                            AutoCreateSqlTable = true
                        })
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .CreateLogger();

                await Infrastructure.Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
                await Infrastructure.Identity.Seeds.DefaultSuperAdmin.SeedAsync(userManager, roleManager);
                await Infrastructure.Identity.Seeds.DefaultBasicUser.SeedAsync(userManager, roleManager);
                Log.Information("Finished Seeding Default Data");
                Log.Information("Application Starting");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
