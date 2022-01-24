using Imengur.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imengur.Controllers
{
    [Route(template: "/api/v1/comments")]
    public class RestCommentController : Controller
    {
        private ICrudCommentRepository crudRepository;

        public RestCommentController(ICrudCommentRepository crudCommentRepository)
        {
            this.crudRepository = crudCommentRepository;
        }

        [HttpGet]
        [Route(template: "{id}")]
        public ActionResult<Comment> Get(int? id)
        {
            if (id == null)
                return BadRequest();

            Comment comment = crudRepository.Find((int)id);
            return comment is null ? NotFound() : comment;
        }

        [HttpPost]
        public ActionResult<Comment> Post([FromBody]Comment newComment)
        {
            //crudRepository.Add(newComment);
            return newComment;
        }
    }
}
