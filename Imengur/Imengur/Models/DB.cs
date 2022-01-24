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
        public DbSet<BetterUser> BetterUsers { get; set; }
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
    public interface IBetterUserRepository
    {
        IQueryable<BetterUser> BetterUsers { get; }
    }

    public interface ICrudBetterUserRepository
    {
        BetterUser Find(string UserName);
        IList<BetterUser> FindAll();

        BetterUser Delete(string Name);
    }

    public class EFUserRepository : IBetterUserRepository
    {
        private ApplicationDbContext _ApplicationDbContext;
        public EFUserRepository(ApplicationDbContext ApplicationDbContext)
        {
            _ApplicationDbContext = ApplicationDbContext;
        }
        public IQueryable<BetterUser> BetterUsers => _ApplicationDbContext.BetterUsers;
    }

    class CrudUserRepository : ICrudBetterUserRepository
    {
        private ApplicationDbContext _context;
        public CrudUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public BetterUser Find(string UserName)
        {
            return _context.BetterUsers.Where(el => el.UserName == UserName).First();
        }

        public BetterUser Delete(string Name)
        {
            var user = _context.BetterUsers.Where(e => e.UserName == Name).First();
            foreach (var image in _context.Images.Where(e => e.BetterUserId == user.Id))
                _context.Remove(image); 
            foreach (var comment in _context.Comments.Where(e => e.BetterUserId == user.Id))
                _context.Remove(comment);

            _context.Remove(user);
            _context.SaveChanges();
            return user;
        }

        public IList<BetterUser> FindAll()
        {
            return _context.BetterUsers.ToList();
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
