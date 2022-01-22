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
        private IBetterUserRepository userRepository;
        private ICustomerImageRepository crudImage;
        private ICrudBetterUserRepository crudUser;

        public CommentController(ICommentRepository repository, 
                                 ICrudCommentRepository crudCommentRepository, 
                                 ICustomerCommentRepository customerRepository,
                                 ICustomerImageRepository crudImage, 
                                 ICrudBetterUserRepository crudUser,
                                 IBetterUserRepository userRepository)
        {
            this.repository = repository;
            this.crudRepository = crudCommentRepository;
            this.customerRepository = customerRepository;
            this.crudImage = crudImage;
            this.crudUser = crudUser;
            this.userRepository = userRepository;
        }

        [Authorize]
        public IActionResult AddComment(Comment comment)
        {
            comment.Date = DateTime.Now;
            comment.BetterUser = crudUser.Find(User.Identity.Name);
            if(comment.Content is not null)
                crudRepository.Add(comment);

            ImagesUsersComments mymodel = new ImagesUsersComments();

            mymodel.Comments = repository.Comments;
            mymodel.BetterUsers = userRepository.BetterUsers;
            mymodel.Image = crudImage.FindById(comment.ImageId);

            return View("/Views/Image/Image.cshtml", mymodel);
        }

        [Authorize]
        public IActionResult DeleteComment(int Id, int ImageId)
        {
            var currentUser = crudUser.Find(User.Identity.Name);
            var currentComment = customerRepository.FindById(Id);
            if ((currentComment is not null && currentUser.Id == currentComment.BetterUserId )|| User.IsInRole("Admin") || User.IsInRole("Moderator"))
                crudRepository.Delete(Id);

            ImagesUsersComments mymodel = new ImagesUsersComments();

            mymodel.Comments = repository.Comments;
            mymodel.BetterUsers = userRepository.BetterUsers;
            mymodel.Image = crudImage.FindById(ImageId);

            return View("/Views/Image/Image.cshtml", mymodel);

        }
    }
}
