using ITLab.Models;
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

        public SessionController(ISessionRepository sessionRepo)
        {
            _sessionRepository = sessionRepo;
        }


        public IActionResult Index(int id)
        {
            Session session = _sessionRepository.GetById(id);
            if (session == null)
                return NotFound();

          //  Console.WriteLine(session.Id + " " + session.Title);
            return View(session);
        }
    }
}
