using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AppleStore.Models;
using AppleStore.Services;
using Store.Entity;
using Store.Entity.Context;
using Store.Repository.Repositories.Interfaces;
using Store.Repository.Repositories;
using Store.Repository.UnitOfWorks;
using Store.Context.Context;
using Currency;

namespace AppleStore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]))
                .AddDbContext<StoreContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddCaching();
            services.AddSession(session =>
            {
                session.IdleTimeout = TimeSpan.FromMinutes(30);
                session.CookieName = ".StoreCookie";
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<IStoreContext, StoreContext>();
            services.AddTransient<IAppleRepository<Apple>, AppleRepository>();
            services.AddTransient<ICategoriesRepository<Categories>, CategoriesRepository>();
            services.AddTransient<IImageRepository<Image>, ImageRepository>();
            services.AddTransient<IAppleColorRepository<AppleColor>, AppleColorRepository>();
            services.AddTransient<IProductDetailsRepository<ProductDetails>, ProductDetailsRepository>();
            services.AddTransient<IDetailNamesRepository<DetailNames>, DetailNamesRepository>();
            services.AddTransient<IColorRepository<Color>, ColorRepository>();
            services.AddTransient<ICurrencyRepository<Store.Entity.Currency>, CurrencyRepository>();
            services.AddTransient<IUnitOfWork,UnitOfWork>();
            services.AddTransient<ICurrencyUSD, CurrencyUSD>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                             .Database.Migrate();
                    }
                }
                catch { }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();
            app.UseSession();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}")
                    .MapRoute(
                    name: "api",
                    template: "api/{controller=Apple}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}

// path - D:\Diplom\Project\AppleStore_v3\src\AppleStore
// cd [folder path (including 'src/projectPath)]
//'dnx' will show if it's ok!
//'dnx ef' will show EntityFramework unicorn and info
//'dnx ef migrations add [name] -c [context]'
//'dnx ef database update -c [context]'   --To update database - contexts have to by in main project!!!!!!!!

//=====restore packages=======
//dnu restore
