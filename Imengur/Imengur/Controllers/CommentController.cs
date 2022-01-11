using Microsoft.AspNetCore.Mvc;
using Imengur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Imengur.Controllers
{
    public class CommentController : Controller
    {
        private ICommentRepository repository;
        private ICrudCommentRepository crudRepository;
        private ICustomerCommentRepository customerRepository;
        private ICrudBetterUserRepository crudUser;

        public CommentController(ICommentRepository repository, ICrudCommentRepository crudCommentRepository, ICustomerCommentRepository customerRepository, ICrudBetterUserRepository crudUser)
        {
            this.repository = repository;
            this.crudRepository = crudCommentRepository;
            this.customerRepository = customerRepository;
            this.crudUser = crudUser;
        }

        [Authorize]
        public IActionResult AddComment(Comment comment)
        {
            Console.WriteLine("TEST");
            comment.Date = DateTime.Now;
            comment.BetterUser = crudUser.Find(User.Identity.Name);
            crudRepository.Add(comment);

            return View("~/Views/Image/SearchForm");
        }
    }
}
