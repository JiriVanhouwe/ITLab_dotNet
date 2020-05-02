using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITLab.Controllers
{
    //roles have to be implemented
    //[Authorize(Roles = "Admin")]
    public class SessionAdminController : Controller
    {

        private ISessionRepository _sessionRepository;

        public SessionAdminController(ISessionRepository sessionRepo)
        {
            _sessionRepository = sessionRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Session> allSessions = _sessionRepository.GetSessions().OrderBy(e => e.Eventdate).ThenBy(e => e.Starthour);

            return View(allSessions);
        }

        public IActionResult OpenSession(int id)
        {
            Session session = _sessionRepository.GetById(id);
            ViewData["SessionInfo"] = session.Title + " op "+ session.Eventdate.ToShortDateString() + " om "+session.Starthour;
            return View();
        }

        [HttpPost, ActionName("OpenSession")]
        public IActionResult OpenSessionConfirmed(int id)
        {
            Session session = _sessionRepository.GetById(id);
            session.openSession();
            _sessionRepository.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}