using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheGoodStuff.Church.ConfigModels;
using TheGoodStuff.Church.Data;
using TheGoodStuff.Church.Data.Models;

namespace TheGoodStuff.Church
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<SiteBasics>( 
                Configuration
                .GetSection("Site")
                .Get<SiteBasics>());

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<ApplicationUser, ApplicationRole>(sa=> 
            { 
                sa.Password.RequiredLength = 6;
                sa.Password.RequireDigit = false;
                sa.Password.RequireLowercase = true;
                sa.Password.RequiredUniqueChars = 2;
                sa.Password.RequireNonAlphanumeric = true;
                sa.Password.RequireUppercase = true;

                sa.Lockout.AllowedForNewUsers = true;
                sa.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                sa.Lockout.MaxFailedAccessAttempts = 5;

                sa.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@.-_+";
                sa.User.RequireUniqueEmail = true;

                sa.SignIn.RequireConfirmedAccount = true;
                sa.SignIn.RequireConfirmedEmail = true;
                sa.SignIn.RequireConfirmedPhoneNumber = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
            });
        }
    }
}
