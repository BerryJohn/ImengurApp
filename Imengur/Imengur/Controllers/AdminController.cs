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
        private UserManager<BetterUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public IActionResult Index()
        {
            return View();
        }
    }
}
