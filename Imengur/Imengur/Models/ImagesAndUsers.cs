using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imengur.Models
{
   public class ViewModelUserImages
    {
        public ICollection<Image> AllImages { get; set; }
        public User CurrentUser { get; set; }
    }
}
