using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Imengur.Models
{
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
    }

    #region Image
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

    public class EFImageRepository : IImageRepository
    {
        private AppIdentityDbContext _AppIdentityDbContext;
        public EFImageRepository(AppIdentityDbContext AppIdentityDbContext)
        {
            _AppIdentityDbContext = AppIdentityDbContext;
        }
        public IQueryable<Image> Images => _AppIdentityDbContext.Images;
    }

    class CrudImageRepository : ICrudImageRepository
    {
        private AppIdentityDbContext _context;
        public CrudImageRepository(AppIdentityDbContext context)
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

        private AppIdentityDbContext context;
        public CustomerImageRepository(AppIdentityDbContext AppIdentityDbContext)
        {
            context = AppIdentityDbContext;
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
    #endregion
    #region User
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
    }

    public interface ICrudUserRepository
    {
        User Find(int id);
        User Delete(int id);
        User Add(User user);
        User Update(User user);

        IList<User> FindAll();
    }

    public interface ICustomerUserRepository
    {
        IList<User> FindByName(string namePattern);
        //IList<User> FindPage(int page, int size);
        User FindById(int id);
        IList<User> FindAll();
    }

    public class EFUserRepository : IUserRepository
    {
        private AppIdentityDbContext _AppIdentityDbContext;
        public EFUserRepository(AppIdentityDbContext AppIdentityDbContext)
        {
            _AppIdentityDbContext = AppIdentityDbContext;
        }
        public IQueryable<User> Users => _AppIdentityDbContext.Users;
    }

    class CrudUserRepository : ICrudUserRepository
    {
        private AppIdentityDbContext _context;
        public CrudUserRepository(AppIdentityDbContext context)
        {
            _context = context;
        }

        public User Find(int id)
        {
            return _context.Users.Find(id);
        }

        public User Delete(int id)
        {
            var product = _context.Users.Remove(Find(id)).Entity;
            _context.SaveChanges();
            return product;
        }

        public User Add(User user)
        {
            var entity = _context.Users.Add(user).Entity;
            _context.SaveChanges();
            return entity;
        }

        public User Update(User user)
        {
            var entity = _context.Users.Update(user).Entity;
            _context.SaveChanges();
            return entity;
        }

        public IList<User> FindAll()
        {
            return _context.Users.ToList();
        }
    }
    class CustomerUserRepository : ICustomerUserRepository
    {

        private AppIdentityDbContext context;
        public CustomerUserRepository(AppIdentityDbContext AppIdentityDbContext)
        {
            context = AppIdentityDbContext;
        }
        public IList<User> FindByName(string namePattern)
        {
            return (from p in context.Users
                    where p.Name.Contains(namePattern)
                    select p).ToList();
        }
        /*public IList<Image> FindPage(int page, int size)
        {
            return (from p in context.Images select p).OrderBy(p => p.Title).Skip((page - 1)
           * size).Take(size).ToList();
        } */
        public User FindById(int id)
        {
            return context.Users.Find(id);
        }
        public IList<User> FindAll()
        {
            return context.Users.ToList();
        }
    }

    #endregion
}
