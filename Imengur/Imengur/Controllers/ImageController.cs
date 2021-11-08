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

        public IActionResult Index()
        {
            return View("ImageList", Images);
        }

        public IActionResult AddForm()
        {
            return View();
        }

        public IActionResult AddImage(Image image)
        {
            if(ModelState.IsValid)
            {
                Images.Add(image);
                return View("ImageList", Images);
            }
            else
            {
                return View("AddForm");
            }
        }

        public IActionResult DeleteImage(Guid Id)
        {
            var imageToRemove = Images.FirstOrDefault(el => Guid.Equals(Id, el.GID));
            if (imageToRemove != null)
                Images.Remove(imageToRemove);
            return View("ImageList", Images);
        }
        
        public IActionResult EditForm(Guid Id)
        {
            var currentImage = Images.FirstOrDefault(el => Guid.Equals(Id, el.GID));
            return View("EditForm", currentImage);
        }

        public IActionResult EditImage(Image image, Guid Id)
        {
            if (ModelState.IsValid)
            {
                var editImage = Images.Find(p => p.GID == Id);
                editImage.Title = image.Title;
                editImage.Description = image.Description;
                return View("ImageList", Images);
            }
            else
            {
                var currentImage = Images.FirstOrDefault(el => Guid.Equals(Id, el.GID));
                return View("EditForm", currentImage);
            }
        }


    }
}
