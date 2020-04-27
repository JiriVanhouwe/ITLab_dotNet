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

            ItlabUser loggedInUser = IUserRepository.LoggedInUser;

            if (loggedInUser == null) //wanneer er niemand ingelogd is
                ViewData["NoUserLoggedIn"] = true;
            else
            {
                ViewData["NoUserLoggedIn"] = false;
                ViewData["UserAlreadyRegistered"] = session.IsUserRegistered(IUserRepository.LoggedInUser.Username); //de ingelogde user is al geregistreerd voor deze sessie
            }

            return View(session);
        }

        [HttpPost]
        public IActionResult RegisterForSession(int id) //TODO werkt nog niet
        {
            Session session = _sessionRepository.GetById(id);
            if (session == null)
                return NotFound();

            ItlabUser loggedInUser = IUserRepository.LoggedInUser;

            try
            {
                session.AddRegisteredUser(new RegisterdUser(session, loggedInUser));
                Console.WriteLine("Hier1");
                _sessionRepository.SaveChanges();
                Console.WriteLine("Hier2");
                _usersRepository.SaveChanges();
                Console.WriteLine("Hier3");

                TempData["message"] = $"Je bent ingeschreven voor deze sessie.";
                 }
                catch
                {
                    TempData["error"] = "Sorry, er ging iets mis...";
                }

            Console.WriteLine("Hier4");
            return RedirectToAction(nameof(Index), id);
        }


    }
}
