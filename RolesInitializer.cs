using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceAgenciesDatabaseApp.Models;
using SpaceAgenciesDatabaseApp.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace SpaceAgenciesDatabaseApp
{
    public class RolesInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "ybelikov@gmail.com";
            string adminPassword = "Asdf1@";
            if(await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if(await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if(await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
