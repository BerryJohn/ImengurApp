using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using Imengur.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imengur.Models
{
    public static class IdentitySeedData
    {
        private const string adminLogin = "adminZbigniew";
        private const string adminPassword = "Qwe12#";

        private const string moderatorLogin = "moderatorPieter";
        private const string moderatorPassword = "Qwe12#";

        /*public static async void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = (UserManager<BetterUser>)scope.ServiceProvider.GetService(typeof(UserManager<BetterUser>));
                BetterUser user = await userManager.FindByIdAsync(adminUser);
                {
                    if (user == null)
                    {
                        //user = new IdentityUser(adminUser);
                        user = new BetterUser
                        {
                            UserName = adminUser,
                        };
                        await userManager.CreateAsync(user, adminPassword);
                    }
                }
            }
        }*/
        public static async void CreateUserRoles(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetRequiredService(typeof(RoleManager<IdentityRole>));
            var userManager = (UserManager<BetterUser>)scope.ServiceProvider.GetRequiredService(typeof(UserManager<BetterUser>));
            IdentityResult roleResult;
            foreach (var roleName in Enum.GetValues(typeof(Roles)))
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName.ToString());

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName.ToString()));
                }
            }

            BetterUser adminUser = await userManager.FindByIdAsync(adminLogin);
            if (adminUser == null)
            {
                adminUser = new BetterUser()
                {
                    UserName = adminLogin,
                };
                await userManager.CreateAsync(adminUser, adminPassword);
            }
            await userManager.AddToRoleAsync(adminUser, Roles.Admin.ToString());


            BetterUser moderatorUser = await userManager.FindByIdAsync(moderatorLogin);
            if (moderatorUser == null)
            {
                moderatorUser = new BetterUser()
                {
                    UserName = moderatorLogin,
                };
                await userManager.CreateAsync(moderatorUser, moderatorPassword);
            }
            await userManager.AddToRoleAsync(moderatorUser, Roles.Moderator.ToString());

        }
    }
}