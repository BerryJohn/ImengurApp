using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imengur.Models
{
    public class Image
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        /*private DateTime _UploadedDate = DateTime.Today; */
        public DateTime UploadDate = DateTime.Today;
        [Required(ErrorMessage = "Image is required")]
        public string ImageData { get; set; }

        public readonly Guid GID = Guid.NewGuid();
    }
}
