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

        public SessionController(ISessionRepository sessionRepo, IUserRepository userRepo)
        {
            _sessionRepository = sessionRepo;
            _usersRepository = userRepo;
        }


        public IActionResult Index(int id)
        {
            Session session = _sessionRepository.GetById(id);
            if (session == null)
                return NotFound();

            ItlabUser loggedInUser = _usersRepository.LoggedInUser;
            //ViewData["ingelogdeuser"] = loggedInUser.Firstname + loggedInUser.Lastname;
            if (loggedInUser == null) //wanneer er niemand ingelogd is
                ViewData["NoUserLoggedIn"] = true;
            else
            {
                ViewData["UserAlreadyRegistered"] = session.IsUserRegistered(_usersRepository.LoggedInUser.Username); //de ingelogde user is al geregistreerd voor deze sessie
            }

            return View(session);
        }

        [HttpPost]
        public IActionResult RegisterForSession(int id) //nog niet getest
        {
            Session session = _sessionRepository.GetById(id);
            if (session == null)
                return NotFound();

            var loggedInUser = _usersRepository.LoggedInUser;

            try
            {
                session.AddRegisteredUser(new RegisterdUser(session, loggedInUser));
                _sessionRepository.SaveChanges();
                _usersRepository.SaveChanges();

                    TempData["message"] = $"Je bent ingeschreven voor deze sessie.";
                 }
                catch
                {
                    TempData["error"] = "Sorry, er ging iets mis...";
                }

            return View(nameof(Index));
        }


    }
}
