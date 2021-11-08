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
        [MinLength(length: 4, ErrorMessage = "Login must be longer than 4")]
        [MaxLength(length: 20, ErrorMessage = "Login cannot be longer than 20")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MinLength(length: 3, ErrorMessage = "Password must be longer than 3")]
        [MaxLength(length: 30, ErrorMessage = "Password cannot be longer than 30")]
        public string Name { get; set; }
        /*[RegularExpression(@".+\\@.+\\.[a-z]{2,3}", ErrorMessage="Email is wrong!")]*/
        [Required(ErrorMessage="Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(length: 6, ErrorMessage = "Password must be longer than 6")]
        [MaxLength(length:50, ErrorMessage="Password cannot be longer than 50")]
        public string Password { get; set; }

        public readonly Guid UID = Guid.NewGuid();
    }
}
