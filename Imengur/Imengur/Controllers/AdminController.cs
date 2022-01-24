using Imengur.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imengur.Controllers
{
    [Authorize]
    [Authorize(Policy = "AdminAccess")]
    public class AdminController : Controller
    {
        private IBetterUserRepository userRepository;
        private ICrudBetterUserRepository crudUser;

        private UserManager<BetterUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public AdminController(ICrudBetterUserRepository crudUser, IBetterUserRepository userRepository,
                               UserManager<BetterUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.crudUser = crudUser;
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminUserPanel()
        {
            return View(userRepository.BetterUsers);
        }

        public IActionResult DeleteUser(string Name)
        {
            var usertoDelete = crudUser.Find(Name);
            if (usertoDelete is not null)
            {
                var currentUser = crudUser.Find(User.Identity.Name);
                if (usertoDelete.Id != currentUser.Id || !User.IsInRole("Admin"))
                    crudUser.Delete(Name);
            }
            return View(userRepository.BetterUsers);
        }
    }
}
