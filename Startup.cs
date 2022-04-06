using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCrashes.Data;
using UTCrashes.Models;

namespace UTCrashes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // default connection
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection")));


            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>() // adding roles
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 20;
            });

            //THIS IS FOR THE CONTENT SECURITY POLICY HEADER
            services.AddControllersWithViews().AddMvcOptions(options =>
            {
                options.InputFormatters.OfType<SystemTextJsonInputFormatter>().First().SupportedMediaTypes.Add(
                    new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/csp-report")
                );
            });

            services.AddDbContext<CrashesDbContext>(options =>
           {
               options.UseMySql(Configuration["ConnectionStrings:UtahCrashesConnection"]);
           });

            services.AddScoped<ICrashesRepository, EFCrashesRepository>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
               //ENABLE HSTS
                app.UseHsts();
            }

            //REDIRECT ALL HTTP TRAFFIC TO HTTPS
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //THIS IS FOR THE CONTENT SECURITY POLICY HEADER
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy-Report-Only",
                    "default-src 'self'; report-uri /cspreport");
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "countypage",
                    "{county}/Page{pageNum}",
                    new { Controller = "Home", action = "AllCrashes" });

                endpoints.MapControllerRoute(
                    "Paging",
                    "page{pageNum}",
                    new { Controller = "Home", action = "AllCrashes", pageNum = 1 });

                endpoints.MapControllerRoute(
                    "county",
                    "{county}",
                    new { Controller = "Home", action = "AllCrashes", pageNum = 1 });

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
            });
        }
    }
}
