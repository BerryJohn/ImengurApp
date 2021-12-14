using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Imengur.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Image> Images { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Userss { get; set; }
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
        private ApplicationDbContext _ApplicationDbContext;
        public EFImageRepository(ApplicationDbContext ApplicationDbContext)
        {
            _ApplicationDbContext = ApplicationDbContext;
        }
        public IQueryable<Image> Images => _ApplicationDbContext.Images;
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
            var image = _context.Images.Remove(Find(id)).Entity;
            _context.SaveChanges();
            return image;
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
        public CustomerImageRepository(ApplicationDbContext ApplicationDbContext)
        {
            context = ApplicationDbContext;
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
        private ApplicationDbContext _ApplicationDbContext;
        public EFUserRepository(ApplicationDbContext ApplicationDbContext)
        {
            _ApplicationDbContext = ApplicationDbContext;
        }
        public IQueryable<User> Users => _ApplicationDbContext.Userss;
    }

    class CrudUserRepository : ICrudUserRepository
    {
        private ApplicationDbContext _context;
        public CrudUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User Find(int id)
        {
            return _context.Userss.Find(id);
        }

        public User Delete(int id)
        {
            var product = _context.Userss.Remove(Find(id)).Entity;
            _context.SaveChanges();
            return product;
        }

        public User Add(User user)
        {
            var entity = _context.Userss.Add(user).Entity;
            _context.SaveChanges();
            return entity;
        }

        public User Update(User user)
        {
            var entity = _context.Userss.Update(user).Entity;
            _context.SaveChanges();
            return entity;
        }

        public IList<User> FindAll()
        {
            return _context.Userss.ToList();
        }
    }
    class CustomerUserRepository : ICustomerUserRepository
    {

        private ApplicationDbContext context;
        public CustomerUserRepository(ApplicationDbContext ApplicationDbContext)
        {
            context = ApplicationDbContext;
        }
        public IList<User> FindByName(string namePattern)
        {
            return (from p in context.Userss
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
            return context.Userss.Find(id);
        }
        public IList<User> FindAll()
        {
            return context.Userss.ToList();
        }
    }

    #endregion

    #region Comment
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }
    }

    public interface ICrudCommentRepository
    {
        Comment Find(int id);
        Comment Delete(int id);
        Comment Add(Comment comments);
        Comment Update(Comment comments);

        IList<Comment> FindAll();
    }

    public interface ICustomerCommentRepository
    {
        //IList<Comment> FindByName(string namePattern);
        //IList<Comment> FindPage(int page, int size);
        Comment FindById(int id);
        IList<Comment> FindAll();
    }

    public class EFCommentRepository : ICommentRepository
    {
        private ApplicationDbContext _ApplicationDbContext;
        public EFCommentRepository(ApplicationDbContext ApplicationDbContext)
        {
            _ApplicationDbContext = ApplicationDbContext;
        }
        public IQueryable<Comment> Comments => _ApplicationDbContext.Comments;
    }

    class CrudCommentRepository : ICrudCommentRepository
    {
        private ApplicationDbContext _context;
        public CrudCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Comment Find(int id)
        {
            return _context.Comments.Find(id);
        }

        public Comment Delete(int id)
        {
            var product = _context.Comments.Remove(Find(id)).Entity;
            _context.SaveChanges();
            return product;
        }

        public Comment Add(Comment comment)
        {
            var entity = _context.Comments.Add(comment).Entity;
            _context.SaveChanges();
            return entity;
        }

        public Comment Update(Comment user)
        {
            var entity = _context.Comments.Update(user).Entity;
            _context.SaveChanges();
            return entity;
        }

        public IList<Comment> FindAll()
        {
            return _context.Comments.ToList();
        }
    }
    class CustomerCommentRepository : ICustomerCommentRepository
    {
        private ApplicationDbContext context;
        public CustomerCommentRepository(ApplicationDbContext ApplicationDbContext)
        {
            context = ApplicationDbContext;
        }
        public Comment FindById(int id)
        {
            return context.Comments.Find(id);
        }
        public IList<Comment> FindAll()
        {
            return context.Comments.ToList();
        }
    }

    #endregion
}
