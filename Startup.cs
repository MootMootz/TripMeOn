using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TripMeOn.BL;
using TripMeOn.BL.interfaces;

namespace TripMeOn
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPropositionService, PropositionService>();
            services.AddScoped<IUserService, UserService>();


            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "TripMeOn.AuthCookie"; // définit le nom du cookie d'authentification
                options.Cookie.HttpOnly = true; // le cookie ne peut être accédé que par le serveur et pas coté client
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // la durée de validité du cookie est 30 minutes
                options.LoginPath = "/Login/IndexClient";
                options.AccessDeniedPath = "/Login/AccessDenied";

            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            using (Models.BddContext ctx = new Models.BddContext())
            {
                ctx.InitializeDb();
            }
            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication(); // Ajouter cette ligne pour activer l'authentification
            app.UseAuthorization();

            app.UseSession(); // Add this line to enable session state           
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=HomePage}/{id?}");
            });


        }
    }
}
// defaults: new { controller = "Home", action = "HomePage" });