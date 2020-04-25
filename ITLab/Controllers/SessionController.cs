using ITLab.Models;
using ITLab.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Controllers
{
    public class SessionController : Controller
    {
        private ISessionRepository _sessionRepository;
        private readonly IUserRepository _usersRepository;
        private UserManager<IdentityUser> _userManager;

        public SessionController(ISessionRepository sessionRepo, IUserRepository userRepo, UserManager<IdentityUser> userManager)
        {
            _sessionRepository = sessionRepo;
            _usersRepository = userRepo;
            _userManager = userManager;
        }


        public IActionResult Index(int id)
        {
            Session session = _sessionRepository.GetById(id);
            if (session == null)
                return NotFound();

            var loggedInUser = _usersRepository.LoggedInUser;
            if (loggedInUser == null) //wanneer er niemand ingelogd is
                ViewData["NoUserLoggedIn"] = true;
            else
            {
                ViewData["UserAlreadyRegistered"] = session.IsUserRegistered(_usersRepository.LoggedInUser.Username); //de ingelogde user is al geregistreerd voor deze sessie
            }

            return View(session);
        }

        public IActionResult RegisterForSession(int id) //TODO
        {
            Session session = _sessionRepository.GetById(id);
            
            try { 

            TempData["message"] = $"Je bent ingeschreven voor deze sessie.";
                 }
                catch
                {
                    TempData["error"] = "Sorry, er ging iets mis...";
                }

            return View();
        }


    }
}
