﻿using Doctrina.Application.System.Commands.SeedSampleData;
using Doctrina.Infrastructure.Identity;
using Doctrina.Persistence;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var config = services.GetRequiredService<IConfiguration>();

                Log.Logger = new LoggerConfiguration()
                   .ReadFrom.Configuration(config)
                  .MinimumLevel.Debug()
                  .Enrich.FromLogContext()
                  .CreateLogger();

                try
                {
                    var doctrinaContext = services.GetRequiredService<DoctrinaDbContext>();
                    doctrinaContext.Database.Migrate();

                    var identityContext = services.GetRequiredService<DoctrinaAuthorizationDbContext>();
                    identityContext.Database.Migrate();

                    var mediator = services.GetRequiredService<IMediator>();
                    await mediator.Send(new SeedSampleDataCommand(), CancellationToken.None);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or initializing the database.");
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }

            host.Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    if (env.IsDevelopment())
                    {
                        var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                        if (appAssembly != null)
                        {
                            config.AddUserSecrets(appAssembly, optional: true);
                        }
                    }

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                 .ConfigureLogging((context, logging) =>
                 {
                     // Clear our default providers
                     logging.ClearProviders();
                 })
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}
