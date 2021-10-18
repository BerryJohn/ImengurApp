using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Imengur.Models
{
    public class User
    {
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        /*[RegularExpression(@".+\\@.+\\.[a-z]{2,3}", ErrorMessage="Email is wrong!")]*/
        [Required(ErrorMessage="Email is required")]
        public string Email { get; set; }
        [MinLength(length:6, ErrorMessage="Password must be longer than 6")]
        [Required(ErrorMessage = "Login is required")]
        public string Password { get; set; }
    }
}
