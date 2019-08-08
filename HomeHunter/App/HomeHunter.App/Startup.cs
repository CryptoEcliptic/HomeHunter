using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Data.DataSeeding;
using HomeHunter.Domain;
using HomeHunter.Infrastructure;
using HomeHunter.Services;
using HomeHunter.Services.Contracts;
using HomeHunter.Infrastructure.EmailSender;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using CloudinaryDotNet;
using HomeHunter.Infrastructure.CloudinaryServices;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

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
                   options.SignIn.RequireConfirmedEmail = false; //TODO set true in production
               })
               .AddEntityFrameworkStores<HomeHunterDbContext>()
               .AddDefaultTokenProviders()
               .AddDefaultUI(UIFramework.Bootstrap4);

            //Cloudinary service
            Account cloudinaryCredentails = new Account(
                this.Configuration["Cloudinary:CloudName"],
                this.Configuration["Cloudinary:ApiKey"],
                this.Configuration["Cloudinary:ApiSecret"]
                );
            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentails);
            services.AddSingleton(cloudinaryUtility);

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IRealEstateTypeServices, RealEstateTypeServices>();
            services.AddTransient<IHeatingSystemServices, HeatingSystemServices>();
            services.AddTransient<IBuildingTypeServices, BuildingTypeServices>();
            services.AddTransient<ICitiesServices, CitiesServices>();
            services.AddTransient<IRealEstateServices, RealEstateServices>();
            services.AddTransient<INeighbourhoodServices, NeighbourhoodServices>();
            services.AddTransient<IAddressServices, AddressServices>();
            services.AddTransient<IVillageServices, VillageServices>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IImageServices, ImageServices>();
            services.AddTransient<IOfferServices, OfferServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ClaimsPrincipal>(s => s.GetService<IHttpContextAccessor>().HttpContext.User);


            services.AddAutoMapper(typeof(HomeHunterProfile));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                })
                .AddMvcOptions(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            
            // Cookie settings
            services
               .ConfigureApplicationCookie(options =>
               {
                   options.Cookie.HttpOnly = true;
                   options.ExpireTimeSpan = TimeSpan.FromHours(7);
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
                
                //Database initial seeding functionality
                new RolesSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
                new RealEstateTypesSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
                new HeatingSystemSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
                new CitiesSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
                new NeighbourhoodSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
                new BuildingTypeSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
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
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(context =>
            {
                context.Response.StatusCode = 404;
                return Task.FromResult(0);
            });
        }
        //TODO Seed All bulgarian cities in the db
    }
}
