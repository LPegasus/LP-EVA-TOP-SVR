using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching;
using LP.Common.Interfaces;
using LP.Common.AuthPolicies;
using Microsoft.AspNetCore.Authorization;

namespace LP
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Role", policy =>
                {
                    policy.Requirements.Add(new RoleConfirmRequirement("Pegasusknight"));
                });
            });
            services.AddSingleton<IAuthorizationHandler, RoleConfirmHandler>();
            services.AddSession(options =>
            {
                options.CookieDomain = "lp-eva.top";
                options.CookieHttpOnly = true;
                options.CookieName = "lp-eva-top";
                options.CookiePath = "path:lp-eva-top";
                options.CookieSecure = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
                options.IdleTimeout = new TimeSpan(0, 5, 0); // 5 minutes
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory
            )
        {
            Common.AppContext.SetApp(app);
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.Use(next => { return new Common.Middlewares.ErrorHandlerMiddleware(next).InvokeAsync; });
            app.Use(next => { return new Common.Middlewares.StopWatchMiddleware(next).InvokeAsync; });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
