using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLab.Models;
using ITLab.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ITLab.Controllers
{
    public class FeedbackController : Controller
    {

        private ISessionRepository _sessionRepository;
        private readonly IUserRepository _usersRepository;


        public FeedbackController(ISessionRepository sessionRepo, IUserRepository userRepo)
        {
            _sessionRepository = sessionRepo;
            _usersRepository = userRepo;
        }

        public IActionResult Index(int id)
        {
            Session session = _sessionRepository.GetById(id);
            if (session == null)
                return NotFound();

            ViewData["sessionId"] = session.Id;
            return View(new FeedbackViewModel() { Title=session.Title });
        }

        [HttpPost]
        public IActionResult Index(int id, FeedbackViewModel feedbackViewModel) 
        {
            //ItlabUser loggedInUser = IUserRepository.LoggedInUser;
            ItlabUser loggedInUser = _usersRepository.GetLoggedInUser();

            Session session = _sessionRepository.GetById(id);
            if (session == null)
                return NotFound();

            try
            {
                if (!session.hasUserAttended(loggedInUser.Username))
                {
                    throw new InvalidOperationException();
                }

                if (!session.Stateenum.Equals(State.FINISHED))
                {
                    throw new InvalidOperationException();
                }

                session.AddFeedback(loggedInUser, feedbackViewModel.Feedback);
                _sessionRepository.SaveChanges();

                TempData["message"] = $"Bedankt voor jouw feedback!";
            }
            catch
            {
                TempData["error"] = "Sorry, er ging iets mis.";
            }
            return RedirectToAction(nameof(Index), id);
        }
    }
}