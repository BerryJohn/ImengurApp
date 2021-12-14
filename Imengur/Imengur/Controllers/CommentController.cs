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
        //static List<Image> Images = new List<Image>();

        private ICommentRepository repository;
        private ICrudCommentRepository crudRepository;
        private ICustomerCommentRepository customerRepository;

        public CommentController(ICommentRepository repository, ICrudCommentRepository crudCommentRepository, ICustomerCommentRepository customerRepository)
        {
            this.repository = repository;
            this.crudRepository = crudCommentRepository;
            this.customerRepository = customerRepository;
        }


        [Authorize]
        public IActionResult AddForm()
        {
            return View();
        }
    }
}
