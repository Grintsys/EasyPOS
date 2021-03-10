using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Grintsys.EasyPOS
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<EasyPOSHttpApiHostModule>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = Configuration["CookiesSettings:LoginPath"];
                options.AccessDeniedPath = Configuration["CookiesSettings:AccessDeniedPath"];
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(Configuration["CookiesSettings:SessionTimeOutTime"]));
                options.Cookie.Name = Configuration["CookiesSettings:CookiesName"];
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}
