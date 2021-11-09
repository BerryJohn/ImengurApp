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
        static List<Image> Images = new List<Image>();

        private IImageRepository repository;
        public ImageController(IImageRepository repository)
        {
            this.repository = repository;
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
                //repository.Images.Append(image);
                Images.Add(image);
                return View("ImageList", repository.Images);
            }
            else
            {
                return View("AddForm");
            }
        }

        public IActionResult DeleteImage(int Id)
        {
            var imageToRemove = Images.FirstOrDefault(el => Id == el.id);
            if (imageToRemove != null)
                Images.Remove(imageToRemove);
            return View("ImageList", Images);
        }
        
        public IActionResult EditForm(int Id)
        {
            var currentImage = Images.FirstOrDefault(el => Id == el.id);
            return View("EditForm", currentImage);
        }

        public IActionResult EditImage(Image image, int Id)
        {
            if (ModelState.IsValid)
            {
                var editImage = Images.Find(p => p.id == Id);
                editImage.Title = image.Title;
                editImage.Description = image.Description;
                return View("ImageList", Images);
            }
            else
            {
                var currentImage = Images.FirstOrDefault(el => Id == el.id);
                return View("EditForm", currentImage);
            }
        }
    }
}
