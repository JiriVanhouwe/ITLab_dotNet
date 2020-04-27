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
           
            ViewData["SessionIsFinshed"] = session.Stateenum.Equals(State.FINISHED);
            ViewData["UserAtend"] = session.AttendeeUser.FirstOrDefault(e => e.UserUsernameNavigation.Equals(loggedInUser)) != null;

            return View(session);
        }

        public IActionResult RegisterForSession(int id) //TODO werkt nog niet
        {
            Session session = _sessionRepository.GetById(id);
            if (session == null)
                return NotFound();

            ItlabUser loggedInUser = IUserRepository.LoggedInUser;

            try
            {
                if(session.RegisterdUser.FirstOrDefault( e => e.UserUsernameNavigation.Equals(loggedInUser)) == null)
                {
                    session.AddRegisteredUser(loggedInUser);
                    TempData["message"] = $"Je bent ingeschreven voor deze sessie.";
                }
                else
                {
                    session.RemoveRegisteredUser(loggedInUser);
                    TempData["message"] = $"Je bent uitgeschreven voor deze sessie.";
                }
                
                
                _sessionRepository.SaveChanges();

                _usersRepository.SaveChanges();
                

                
                 }
                catch
                {
                    TempData["error"] = "Sorry, er ging iets mis...";
                }

            return RedirectToAction("index", new {  id });
        }


    }
}
