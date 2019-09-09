using Doctrina.Application.Interfaces;
using Doctrina.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace Doctrina.WebUI
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
           .AddEnvironmentVariables()
           .Build();

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging((context, logging) => {
                    // Clear our default providers
                    logging.ClearProviders();

                    //logging.AddConfiguration(Configuration.GetSection("Logging"));
                    //logging.AddDebug();
                    //logging.AddEventSourceLogger();
                })
                .UseSerilog()
                .Build();
        }

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
               .MinimumLevel.Debug()
               .Enrich.FromLogContext()
               .CreateLogger();

            try
            {
                var host = BuildWebHost(args);

                using (var scope = host.Services.CreateScope())
                {
                    try
                    {
                        var context = scope.ServiceProvider.GetService<IDoctrinaDbContext>();
                        var concreteContext = (DoctrinaDbContext)context;

                        if (concreteContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                        {
                            concreteContext.Database.Migrate();
                        }

                        var initializer = scope.ServiceProvider.GetService<IDoctrinaInitializer>();
                        initializer.SeedEverything();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "An error occurred while migrating or initializing the database.");
                    }
                }

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }


    }
}
