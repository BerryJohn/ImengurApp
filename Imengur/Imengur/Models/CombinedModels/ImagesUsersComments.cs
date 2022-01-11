using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using PagedList;

namespace Imengur.Models
{
    public class ImagesUsersComments
    {
        public IQueryable<BetterUser> BetterUsers { get; set; }
        public Image Image { get; set; }
        public Comment Comment { get; set; }
        public IQueryable<Comment> Comments { get; set;}
    }
}