using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Imengur.Models
{
    public interface IImageRepository
    {
        IQueryable<Image> Images { get; }
    }

    public interface ICrudImageRepository
    {
        Image Find(int id);
        Image Delete(int id);
        Image Add(Image image);
        Image Update(Image image);

        IList<Image> FindAll();
    }

    public interface ICustomerImageRepository
    {
        IList<Image> FindByName(string namePattern);
        //IList<Image> FindPage(int page, int size);
        Image FindById(int id);
        IList<Image> FindAll();
    }
    public class Image
    {

        [Required(ErrorMessage = "Title is required")]
        [MinLength(3, ErrorMessage = "Title minimum length is 3")]
        [StringLength(100, ErrorMessage = "Title cannot be longer then 100")]
        public string Title { get; set; }

        //public DateTime UploadDate = DateTime.Today;

        [FileExtensions(Extensions = "jpg,png")]
        [Required(ErrorMessage = "Image is required")]
        public string ImageData { get; set; }

        [StringLength(100, ErrorMessage = "Description cannot be longer then 100")]
        public string Description { get; set; }

        [Key]
        public int id { get; set; }
    }

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options){ }
        public DbSet<Image> Images { get; set; }
    }

    public class EFImageRepository : IImageRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public EFImageRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IQueryable<Image> Images => _applicationDbContext.Images;
    }

    class CrudImageRepository : ICrudImageRepository
    {
        private ApplicationDbContext _context;
        public CrudImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Image Find(int id)
        {
            return _context.Images.Find(id);
        }

        public Image Delete(int id)
        {
            var product = _context.Images.Remove(Find(id)).Entity;
            _context.SaveChanges();
            return product;
        }

        public Image Add(Image image)
        {
        var entity = _context.Images.Add(image).Entity;
        _context.SaveChanges();
        return entity;
        }

        public Image Update(Image image)
        {
        var entity = _context.Images.Update(image).Entity;
        _context.SaveChanges();
        return entity;
        }

        public IList<Image> FindAll()
        {
        return _context.Images.ToList();
        }
    }
    class CustomerImageRepository : ICustomerImageRepository
    {

        private ApplicationDbContext context;
        public CustomerImageRepository(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        public IList<Image> FindByName(string namePattern)
        {
            return (from p in context.Images
                    where p.Title.Contains(namePattern)
                    select p).ToList();
        }
        /*public IList<Image> FindPage(int page, int size)
        {
            return (from p in context.Images select p).OrderBy(p => p.Title).Skip((page - 1)
           * size).Take(size).ToList();
        } */
        public Image FindById(int id)
        {
            return context.Images.Find(id);
        }
        public IList<Image> FindAll()
        {
            return context.Images.ToList();
        }
    }

}

