using Microsoft.AspNetCore.Mvc;
using Imengur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Imengur.Controllers
{
    public class ImageController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        //static List<Image> Images = new List<Image>();

        private IImageRepository repository;
        private ICrudImageRepository crudRepository;
        private ICustomerImageRepository customerRepository;
        private IBetterUserRepository userRepository;
        private ICrudBetterUserRepository crudUser;

        public ImageController(IImageRepository repository, 
                               ICrudImageRepository crudImageRepository, 
                               ICustomerImageRepository customerRepository, 
                               IHostingEnvironment environment,
                               IBetterUserRepository userRepository,
                               ICrudBetterUserRepository crudUser
                               )
        {
            this.repository = repository;
            this.crudRepository = crudImageRepository;
            this.customerRepository = customerRepository;
            hostingEnvironment = environment;
            this.userRepository = userRepository;
            this.crudUser = crudUser;
        }

        public IActionResult Index(int? page)
        {
            ImageAndUsers mymodel = new ImageAndUsers();

            int pageNumber = (page ?? 1);

            mymodel.Images = repository.Images.ToPagedList(pageNumber, 10);
            mymodel.BetterUsers = userRepository.BetterUsers;

            return View("ImageList", mymodel);
        }

        [Authorize]
        public IActionResult AddForm()
        {
            return View();
        }

        [Authorize]
        public IActionResult AddImage(Image image, int? page)
        {
            IFormFile fileUpload = image.ImageFile;
            
            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    var uniqueFileName = GetUniqueFileName(fileUpload.FileName);
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    fileUpload.CopyTo(new FileStream(filePath, FileMode.Create));
                    image.BetterUser = crudUser.Find(User.Identity.Name);
                    image.ImageData = uniqueFileName;
                    crudRepository.Add(image);
                    int pageNumber = (page ?? 1);
                    return View("ImageList", repository.Images.ToPagedList(pageNumber, 10));
                }
                else
                    return View("AddForm");
            }
            else
            {
                return View("AddForm");
            }
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }


        [Authorize]
        public IActionResult DeleteImage(int Id, int? page)
        {
            crudRepository.Delete(Id);
            int pageNumber = (page ?? 1);
            return View("ImageList", repository.Images.ToPagedList(pageNumber, 10));
        }

        [Authorize]
        public IActionResult EditForm(int Id, int? page)
        {
            var currentImage = crudRepository.Find(Id);
            return View("EditForm", currentImage);
        }

        [Authorize]
        public IActionResult EditImage(Image image, int? page)
        {
            if (ModelState.IsValid)
            {
                crudRepository.Update(image);
                int pageNumber = (page ?? 1);
                return View("ImageList", repository.Images.ToPagedList(pageNumber, 10));
            }
            else
            {
                return View("EditForm", image);
            }
        }

        public IActionResult SearchForm()
        {
            return View("SearchForm");
        }

        public IActionResult SearchImage(string? title, string? id)
        {
            int newId;
            if (int.TryParse(id, out newId))
            {
                var result = customerRepository.FindById(newId);
                return View("Image", result);
            }
            else if (!string.IsNullOrEmpty(title))
            {
                var result = customerRepository.FindByName(title);
                return View("SearchList", result);
            }
            else
            {
                return View("SearchList", customerRepository.FindAll());
            }
        }
    }
}
