using AutoMapper;
using Doctrina.Application.Infrastructure;
using Doctrina.Application.Infrastructure.AutoMapper;
using Doctrina.Application.Interfaces;
using Doctrina.Application.Statements.Commands;
using Doctrina.Domain.Identity;
using Doctrina.Persistence;
using Doctrina.WebUI.Filters;
using Doctrina.xAPI.Store.Builder;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;
using System;
using System.IO;
using System.Reflection;

namespace Doctrina.WebUI
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            //services.AddCustomMapper();

            // Add framework services.
            //services.AddTransient<INotificationService, NotificationService>();
            //services.AddTransient<IDateTime, MachineDateTime>();

            // Add httpcontext
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add MediatR
            services.AddMediatR(typeof(CreateStatementCommand).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            // Add DbContext using SQL Server Provider
#if DEBUG
            services.AddDbContext<DoctrinaDbContext>(options =>
                options.UseInMemoryDatabase("Doctrina"));

#else
            services.AddDbContext<IDoctrinaDbContext, DoctrinaDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DoctrinaDatabase")));
#endif

            // Register the service and implementation for the database context
            services.AddScoped<IDoctrinaDbContext>(provider => provider.GetService<DoctrinaDbContext>());
            services.AddTransient<IDoctrinaInitializer, DoctrinaInitializer>();

            ConfigureIdentity(services);

            // Enable the use of an [Authorize("Bearer")] attribute on methods and
            // classes to protect.
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser()
                    .Build());
            });

            services.AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateStatementsCommandValidator>());

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddLearningRecordStore();
        }

        private static void ConfigureIdentity(IServiceCollection services)
        {
            services.AddIdentity<DoctrinaUser, DoctrinaRole>()
                            .AddEntityFrameworkStores<DoctrinaDbContext>()
                            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 2;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDoctrinaInitializer initializer)
        {
            if (env.IsDevelopment())
            {
                _logger.LogInformation("In Development environment");
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Serilog Version 3.0.0.*
            //app.UseSerilogRequestLogging();

            // Handle Lets Encrypt Route (before MVC processing!)
            app.UseRouter(r =>
            {
                r.MapGet(".well-known/acme-challenge/{id}", async (request, response, routeData) =>
                {
                    var id = routeData.Values["id"] as string;
                    var file = Path.Combine(env.WebRootPath, ".well-known", "acme-challenge", id);
                    await response.SendFileAsync(file);
                });
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });


            // Use Cookie Policy Middleware to conform to EU General Data
            // Protection Regulation (GDPR) regulations.
            app.UseCookiePolicy();



            app.UseLearningRecordStore();

            // If the app uses session state, call Session Middleware after Cookie
            // Policy Middleware and before MVC Middleware.
            //app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=home}/{action=index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
