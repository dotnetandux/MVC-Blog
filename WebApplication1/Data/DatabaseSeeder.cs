using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class DatabaseSeeder
    {
        private static string adminRole = "Admin";
        private static string userRole = "User";
        private static string password = "Password123!";

        public static async Task Seed(ApplicationDbContext context, UserManager<ApplicationUser> uManager, RoleManager<ApplicationRole> rManager)
        {
            context.Database.EnsureCreated();
            await SeedRoles(rManager);
            await SeedUsers(uManager, rManager);
        }

        private static async Task SeedRoles(RoleManager<ApplicationRole> rManager)
        {
            if (await rManager.FindByNameAsync(adminRole) == null)
            {
                await rManager.CreateAsync(new ApplicationRole(adminRole));
            }

            if (await rManager.FindByNameAsync(userRole) == null)
            {
                await rManager.CreateAsync(new ApplicationRole(userRole));
            }
        }

        private static async Task SeedUsers(UserManager<ApplicationUser> uManager, RoleManager<ApplicationRole> rManager)
        {
            // Admin
            string AdminName = "Member1";

            if (await uManager.FindByNameAsync(AdminName) == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = AdminName,
                    Email = AdminName + "@email.com",
                    FirstName = "",
                    LastName = "",
                    Role = adminRole
                };

                var result = await uManager.CreateAsync(admin);
                if (result.Succeeded)
                {
                    await uManager.AddPasswordAsync(admin, password);
                    await uManager.AddToRoleAsync(admin, adminRole);
                }
            }

            // Users
            string userName = "Customer";

            for (int i = 1; i < 6; i++)
            {
                if (await uManager.FindByNameAsync(userName + i) == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = userName + i,
                        Email = userName + i + "@email.com",
                        FirstName = "",
                        LastName = "",
                        Role = userRole
                    };

                    var result = await uManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        await uManager.AddPasswordAsync(user, password);
                        await uManager.AddToRoleAsync(user, userRole);
                    }
                }
            }
        }
    }
}
