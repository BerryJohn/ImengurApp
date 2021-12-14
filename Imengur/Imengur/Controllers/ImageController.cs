using Microsoft.AspNetCore.Mvc;
using Imengur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using Microsoft.AspNetCore.Authorization;

namespace Imengur.Controllers
{
    public class ImageController : Controller
    {
        //static List<Image> Images = new List<Image>();

        private IImageRepository repository;
        private ICrudImageRepository crudRepository;
        private ICustomerImageRepository customerRepository;

        public ImageController(IImageRepository repository, ICrudImageRepository crudImageRepository, ICustomerImageRepository customerRepository)
        {
            this.repository = repository;
            this.crudRepository = crudImageRepository;
            this.customerRepository = customerRepository;
        }

        public IActionResult Index(int? page)
        {

            int pageNumber = (page ?? 1);
            return View("ImageList", repository.Images.ToPagedList(pageNumber,10));
        }

        [Authorize]
        public IActionResult AddForm()
        {
            return View();
        }

        [Authorize]
        public IActionResult AddImage(Image image, int? page)
        {
            if(ModelState.IsValid)
            {
                crudRepository.Add(image);
                int pageNumber = (page ?? 1);
                return View("ImageList", repository.Images.ToPagedList(pageNumber, 10));
            }
            else
            {
                return View("AddForm");
            }
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
