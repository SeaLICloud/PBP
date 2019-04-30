using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PBP.Web.Models.Context;
using PBP.Web.Models.Domain;

namespace PBP.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMemoryCache();
            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            var connection = @"Data Source=.\SQLEXPRESS;Initial Catalog=PBPDB;Integrated Security=True";
            services.AddDbContext<AccountContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<OrganizationContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<PartyMemberContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<AccountPartyMemberContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<PartyCostContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<PartyCostRecordContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<FileContext>(options => options.UseSqlServer(connection));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}