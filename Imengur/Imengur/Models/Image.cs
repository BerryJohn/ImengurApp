using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Imengur.Models
{
    public class Image
    {
        [Required(ErrorMessage = "Title is required")]
        [MinLength(3, ErrorMessage = "Title minimum length is 3")]
        [StringLength(100, ErrorMessage = "Title cannot be longer then 100")]
        public string Title { get; set; }


        [FileExtensions(Extensions = "jpg,png")]
/*        [Required(ErrorMessage/ = "Image is required")]*/
        public string ImageData { get; set; }

        [StringLength(100, ErrorMessage = "Description cannot be longer then 100")]
        public string Description { get; set; }

        [Key]
        public int Id { get; set; }
   
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }

        public string BetterUserId { get; set; }
        public BetterUser BetterUser { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }

}

