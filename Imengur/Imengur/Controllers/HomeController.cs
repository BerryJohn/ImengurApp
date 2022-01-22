using Imengur.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

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

        public IActionResult Index()
        {
            ImageAndUsersHome mymodel = new ImageAndUsersHome();

            mymodel.Images = repository.Images;
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
