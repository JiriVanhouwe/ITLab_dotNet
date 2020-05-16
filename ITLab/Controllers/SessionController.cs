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
                ViewData["UserLoggedIn"] = false;
            else
            {
                ViewData["UserLoggedIn"] = true;
                ViewData["UserAlreadyRegistered"] = session.IsUserRegistered(IUserRepository.LoggedInUser.Username); //de ingelogde user is al geregistreerd voor deze sessie
                ViewData["UserAttended"] = session.AttendeeUser.Any(e => e.UserUsername == loggedInUser.Username);
               // ViewData["DeadlineForFeedback"] = session.GiveDeadlineForFeedback();
            }
           
            ViewData["SessionIsFinished"] = session.Stateenum.Equals(State.FINISHED);

            if (session.Feedback.Count > 0)
                ViewData["Feedback"] = session.Feedback;

            return View(session);
        }

        public IActionResult RegisterForSession(int id)
        {
            Session session = _sessionRepository.GetById(id);
            if (session == null)
                return NotFound();

            ItlabUser loggedInUser = IUserRepository.LoggedInUser;
            try
            {
                if(session.RegisterdUser.Any(e => e.UserUsernameNavigation.Equals(loggedInUser)))
                {
                    session.RemoveRegisteredUser(loggedInUser);
                    TempData["message"] = $"Je bent uitgeschreven voor deze sessie.";
                }
                else
                {
                    session.AddRegisteredUser(loggedInUser);
                    TempData["message"] = $"Je bent ingeschreven voor deze sessie.";
                   
                }       
                _sessionRepository.SaveChanges();
                _usersRepository.SaveChanges();
                 
            }
            catch(Exception e)
                { 
                    if(e is ArgumentException)
                {
                    TempData["error"] = e.Message;
                } 
                else 
                    TempData["error"] = "Sorry, er ging iets mis...";
                }

            return RedirectToAction("index", new {  id });
        }
    }
}
