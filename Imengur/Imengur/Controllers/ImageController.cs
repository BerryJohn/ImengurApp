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
        public Guid LastGuid;

        public IActionResult Index()
        {
            return View();
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
        public IActionResult DeleteImage(string id)
        {
            //var itemToRemove = Images.FirstOrDefault(el => el.GID)
            System.Diagnostics.Debug.WriteLine("huj");
            System.Diagnostics.Debug.WriteLine(id);

            //Images.Remove(itemToRemove);
 
            return View("ImageList", Images);
        }

    }
}
