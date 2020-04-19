using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ITLab.Models;

namespace ITLab.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _usersRepo;
        private readonly ISessionRepository _sessionRepository;

        public HomeController(IUserRepository userRepo, ISessionRepository sessionRepo)
        {
            _usersRepo = userRepo;
            _sessionRepository = sessionRepo;
        }

        public IActionResult Index()
        {
            Session session = _sessionRepository.GetFirstComingSession();
            if (session == null)
                return NotFound(); //TODO wat als er geen komende sessie is? Dan verandert de view?
            return View(session);
        }
    }
}
