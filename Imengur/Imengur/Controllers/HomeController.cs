using Imengur.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using PagedList;
using System.Linq;

namespace Imengur.Controllers
{
    public class HomeController : Controller
    {
        private IImageRepository repository;
        private IBetterUserRepository userRepository;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IImageRepository repository, IBetterUserRepository userRepository)
        {
            _logger = logger;
            this.repository = repository;
            this.userRepository = userRepository;
        }

        public IActionResult Index(int pageNumber = 1)
        {
            ImageAndUsers mymodel = new ImageAndUsers();

            var images = repository.Images.ToList();

            mymodel.Images = Enumerable.Reverse(images).ToPagedList(pageNumber, 5);
            mymodel.BetterUsers = userRepository.BetterUsers;

            return View(mymodel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
