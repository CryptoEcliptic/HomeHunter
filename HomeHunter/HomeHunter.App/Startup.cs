using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeHunter.Data;
using HomeHunter.Data.DataSeeding;
using HomeHunter.Domain;
using HomeHunter.Services.EmailSender;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeHunter.App
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
            services.AddDbContext<HomeHunterDbContext>(
              options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services
               .AddIdentity<HomeHunterUser, IdentityRole>(options =>
               {
                   options.Password.RequireDigit = false;
                   options.Password.RequireLowercase = false;
                   options.Password.RequireUppercase = false;
                   options.Password.RequireNonAlphanumeric = false;
                   options.Password.RequiredLength = 6;
                   options.SignIn.RequireConfirmedEmail = true; //TODO set false during development
               })
               .AddEntityFrameworkStores<HomeHunterDbContext>()
               .AddDefaultTokenProviders()
               .AddDefaultUI(UIFramework.Bootstrap4);

            services.AddTransient<IEmailSender, EmailSender>();
            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            // Cookie settings
            services
               .ConfigureApplicationCookie(options =>
               {
                   options.Cookie.HttpOnly = true;
                   options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                   options.LoginPath = "/Identity/Account/Login";
                   options.LogoutPath = "/Identity/Account/Logout";
               });

            services.AddSingleton(this.Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {

                var dbContext = serviceScope.ServiceProvider.GetRequiredService<HomeHunterDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.EnsureCreated();
                }
                
                new RolesSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //Add authentication in the request pipeline
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        //TODO Implement Email confirm functionality
        //TODO Decide weather to remove User City Name
    }
}
