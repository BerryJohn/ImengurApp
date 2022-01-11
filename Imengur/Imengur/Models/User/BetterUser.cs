using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Imengur.Models
{
    public class BetterUser : IdentityUser
    {

        public BetterUser() : base() { }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}