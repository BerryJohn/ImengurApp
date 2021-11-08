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
            return View("UserList", Users);
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

        public IActionResult DeleteUser(Guid Id)
        {
            var userToRemove = Users.FirstOrDefault(el => Guid.Equals(Id, el.UID));
            if (userToRemove != null)
                Users.Remove(userToRemove);
            return View("UserList", Users);
        }

        public IActionResult EditUserForm(Guid Id)
        {
            var currentUser = Users.FirstOrDefault(el => Guid.Equals(Id, el.UID));
            return View("EditUserForm", currentUser);
        }
        
        //public IActionResult EditUser(User user, Guid Id)
        public IActionResult EditUser(string Login, string Name, string Email, string Password, bool NewPassword, Guid Id)
        {
             if (ModelState.IsValid)
             {
                 Users.Find(p => p.UID == Id).Login = Login;
                 Users.Find(p => p.UID == Id).Name = Name;
                 Users.Find(p => p.UID == Id).Email = Email;
                 if(NewPassword)
                    Users.Find(p => p.UID == Id).Password = Password;
                 return View("UserList", Users);
             }
             else
             {
                 var currentUser = Users.FirstOrDefault(el => Guid.Equals(Id, el.UID));
                 return View("EditUserForm", currentUser);
             }


            //return View("UserList", Users); 
        }
    }
}
