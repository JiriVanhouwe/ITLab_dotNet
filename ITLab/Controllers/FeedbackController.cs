using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLab.Models;
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

            return View(session);
        }

        [HttpPost]
        public IActionResult GiveFeedback(int id)
        {

            return View();


        }
    }
}