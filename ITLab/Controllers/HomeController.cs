using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ITLab.Models;
using TestDatabase.Models;

namespace ITLab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _usersRepo;

        public HomeController(ILogger<HomeController> logger, IUserRepository rep)
        {
            _logger = logger;
            _usersRepo = rep;
        }

        public IActionResult Index()
        {
            ItlabUser user = _usersRepo.GetById();
            ViewData["user"] = user.Firstname + user.UserStatus;
            //TEST COMMIT
            return View();
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
