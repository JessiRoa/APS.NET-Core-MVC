using Microsoft.AspNetCore.Identity;
using RoutingWebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutingWebbApp.Data.Migrations
{
    public class DBSeeder
    {
        public static void Seed(ApplicationDbContext context,
            UserManager<ApplicationUser>userManager,
             RoleManager<IdentityRole> roleManager)
        {
            if (!context.RoleClaims.Any())
            {
                var admin = new IdentityRole { Name = "Admin" };
                var result = roleManager.CreateAsync(admin);
            }
            if (!context.Users.Any())
            {
                var user = new ApplicationUser { UserName = "student@test.com", Email = "student@test.com" };
                var result = userManager.CreateAsync(user, "Pa$$w0rd").Result;
                var roleresult = userManager.AddToRoleAsync(user, "Admin").Result;
            }
        }
    }
}
