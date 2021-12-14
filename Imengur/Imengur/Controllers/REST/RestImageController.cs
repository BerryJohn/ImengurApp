using Imengur.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imengur.Controllers
{
    [Route(template: "/api/v1/images")]
    public class RestImageController : Controller
    {
        private ICrudImageRepository crudRepository;

        public RestImageController(ICrudImageRepository crudImageRepository)
        {
            this.crudRepository = crudImageRepository;
        }

        [HttpGet]
        [Route(template:"{id}")]
        public ActionResult<Image> Get(int ?id)
        {
            if(id == null)
                return BadRequest();

            Image image = crudRepository.Find((int)id);
            return image is null ? NotFound() : image;
        }
    }
}
