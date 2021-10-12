using Imengur.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imengur.Controllers
{
    public class UserController : Controller
    {
        static List<User> Users = new List<User>();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddForm()
        {
            return View();
        }

        public IActionResult AddUser(User user)
        {
            if(ModelState.IsValid)
            {
                Users.Add(user);
                return View("UserList", Users);
            }
            else
            {
                return View("AddForm");
            }
        }
    }
}
