using MajorProject.Data;
using MajorProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajorProject
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
            services
               .AddDbContext<ApplicationDbContext>(options =>
               {
                   // Get the SQL Connection String from the AppSettings.json file
                   string connString = Configuration.GetConnectionString("MyDefaultConnectionString");

                   // Register EntityFramework Core Services to use SQL Server
                   options.UseSqlServer(connString);
               });

            services
                .AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequiredLength = 8;

                }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Login");
                });


            services
                .ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);      // Default Session Cookie expiry is 20 minutes
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Name = "MyAuthCookie";
                });


            services
                .AddSingleton<IEmailSender, MyEmailSenderService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
            , RoleManager<IdentityRole> rolemanager,
            UserManager<IdentityUser> usermanager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area}/{controller=Home}/{action=Index}/{id?}");

                // Register the endpoints for the ROUTES not in any area.
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            ApplicationDbContextSeed.SeedIdentityRolesAsync(rolemanager).Wait();
            ApplicationDbContextSeed.SeedIdentityUserAsync(usermanager).Wait();
        }
    }
}
