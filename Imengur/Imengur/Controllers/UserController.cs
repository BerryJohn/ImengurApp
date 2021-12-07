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
        //static List<User> Users = new List<User>();

        private IUserRepository repository;
        private ICrudUserRepository crudRepository;
        private ICustomerUserRepository customerRepository;

        public UserController(IUserRepository repository, ICrudUserRepository crudUserRepository, ICustomerUserRepository customerRepository)
        {
            this.repository = repository;
            this.crudRepository = crudUserRepository;
            this.customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            return View("UserList", repository.Users);
        }

        public IActionResult AddForm()
        {
            return View();
        }

        public IActionResult AddUser(User user)
        {
            if(ModelState.IsValid)
            {
                crudRepository.Add(user);
                return View("UserList", repository.Users);
            }
            else
            {
                return View("AddForm");
            }
        }

        public IActionResult DeleteUser(int Id)
        {
            crudRepository.Delete(Id);
            return View("UserList", repository.Users);
        }

        public IActionResult EditUserForm(int Id)
        {
            var currentUser = crudRepository.Find(Id);
            return View("EditUserForm", currentUser);
        }
        
        //public IActionResult EditUser(User user, Guid Id)
        public IActionResult EditUser(User user)
        {
             if (ModelState.IsValid)
             {
                crudRepository.Update(user);
                 return View("UserList", repository.Users);
             }
             else
             {
                return View("EditUserForm", user);
             }
            //return View("UserList", Users); 
        }

        public IActionResult SearchForm()
        {
            return View("SearchForm");
        }

        public IActionResult SearchUserImages(string? id)
        {
            int newId;
            if (int.TryParse(id, out newId))
            {

                var result = customerRepository.FindById(newId);
                return View("UserImages", result);
            }
            return View("UserList", repository.Users);

        }
    }
}
