using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Data.seedingData
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Seed Roles
            string[] roleNames = { "Admin", "Manager", "Sales" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Admin User
            var adminUser = new IdentityUser
            {
                UserName = "admin@galaxystore.com",
                Email = "admin@galaxystore.com",
                EmailConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != adminUser.Id))
            {
                var user = await userManager.FindByEmailAsync(adminUser.Email);
                if (user == null)
                {
                    var adminPassword = "Admin@123";
                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
            }
        }
    }
}
