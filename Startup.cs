using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TripMeOn.BL;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;

namespace TripMeOn
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddAuthentication("CookieAuthentication") // ajoute le service d'authentification qui utilise l'authentification par cookie
                .AddCookie("CookieAuthentication", options =>
                {
                    options.Cookie.Name = "TripMeOn.AuthCookie"; // d�finit le nom du cookie d'authentification
                    options.Cookie.HttpOnly = true; // le cookie ne peut �tre acc�d� que par le serveur et pas cot� client
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // la dur�e de validit� du cookie est 30 minutes
                    options.LoginPath = "/Login/LoginClient";
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