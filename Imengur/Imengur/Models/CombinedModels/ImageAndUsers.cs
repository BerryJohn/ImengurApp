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
    public class ImageAndUsersHome
    {
        public IQueryable<BetterUser> BetterUsers { get; set; }
        public IQueryable<Image> Images { get; set; }
    }
}