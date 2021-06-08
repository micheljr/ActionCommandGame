using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActionCommandGame.Repository;
using ActionCommandGame.Services;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActionCommandGame.UI.Mvc
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
            // Register EF sql-database voor game
            var gameConnectionString = Configuration.GetConnectionString("gameDatabaseTest");
            services.AddDbContext<ActionButtonGameUiDbContext>(options =>
            {
                options.UseSqlServer(gameConnectionString);
            });

            // Register EF sql-database voor users
            var userConnectionString = Configuration.GetConnectionString("userDatabaseTest");
            services.AddDbContext<ActionButtonUsersDbContext>(options =>
            {
                options.UseSqlServer(userConnectionString);
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ActionButtonUsersDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Account/login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IPositiveGameEventService, PositiveGameEventService>();
            services.AddScoped<INegativeGameEventService, NegativeGameEventService>();
            services.AddScoped<IPlayerItemService, PlayerItemService>();
            services.AddScoped<IGameService, GameService>();
            
            var appSettings = new AppSettings();
            Configuration.Bind(nameof(AppSettings), appSettings);
            services.AddSingleton<AppSettings>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
            });
        }
    }
}