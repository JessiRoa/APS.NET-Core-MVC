using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoutingWebbApp.Data;
using RoutingWebbApp.Models;
using RoutingWebbApp.Services;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using RoutingWebbApp.Data.Migrations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RoutingWebbApp
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new RequireHttpsAttribute()); //Si comento estas lineas puedo köra programmet med RoutingWebbApp
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}/{slug?}");
            });

            DBSeeder.Seed(context, userManager, roleManager); // DbSeed.Seed(context);
        }
    }
}
