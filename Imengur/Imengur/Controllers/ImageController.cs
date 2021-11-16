using Microsoft.AspNetCore.Mvc;
using Imengur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public IActionResult Index()
        {
            return View("ImageList", repository.Images);
        }

        public IActionResult AddForm()
        {
            return View();
        }

        public IActionResult AddImage(Image image)
        {
            if(ModelState.IsValid)
            {
                crudRepository.Add(image);
                return View("ImageList", repository.Images);
            }
            else
            {
                return View("AddForm");
            }
        }

        public IActionResult DeleteImage(int Id)
        {
            crudRepository.Delete(Id);
            return View("ImageList", repository.Images);
        }
        
        public IActionResult EditForm(int Id)
        {
            var currentImage = crudRepository.Find(Id);
            return View("EditForm", currentImage);
        }

        public IActionResult EditImage(Image image)
        {
            if (ModelState.IsValid)
            {
                crudRepository.Update(image);
                return View("ImageList", repository.Images);
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
            if (!string.IsNullOrEmpty(title))
            {
                var result = customerRepository.FindByName(title);
                return View("SearchList", result);
            }
            else if( int.TryParse(id, out newId))
            {
                var result = customerRepository.FindById(newId);
                return View("Image", result);
            }
            else
            {
                return View("SearchList", customerRepository.FindAll());
            }

        }
    }
}
