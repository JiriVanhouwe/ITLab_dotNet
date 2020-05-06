using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLab.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITLab.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ISessionRepository _sessionRepository;

        public CalendarController(ISessionRepository sessionRepo)
        {
            _sessionRepository = sessionRepo;
            _sessionRepository.UpdateFinishedSessions();
        }

        public IActionResult Index()
        {

            IEnumerable<Session> allSessions = _sessionRepository.GetSessions();

            return View(allSessions);
        }
       
    }
}