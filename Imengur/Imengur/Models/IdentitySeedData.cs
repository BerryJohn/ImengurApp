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
        private const string adminUser = "admintest";
        private const string adminPassword = "iX5sEx/WtKIhiOePjpTMo/RTqO8vp81L58xo8cz9qGc="; //123

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>));
                IdentityUser user = await userManager.FindByIdAsync(adminUser);
                {
                    if (user == null)
                    {
                        user = new IdentityUser(adminUser);
                        await userManager.CreateAsync(user, adminPassword);
                    }
                }
            }
        }
    }
}