using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imengur.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "admin";
        private const string adminPassword = "123"; //123

        public static async void EnsurePopulated(IApplicationBuilder app)
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
        }
    }
}