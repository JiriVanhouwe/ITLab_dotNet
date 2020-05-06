using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataAnnotationsExtensions;
using ITLab.Models;
using ITLab.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITLab.Controllers
{
    
    
    public class SessionAdminController : Controller
    {

        private ISessionRepository _sessionRepository;
        private IUserRepository _userRepository;

       
        public SessionAdminController(ISessionRepository sessionRepo, IUserRepository userRepository)
        {
            _sessionRepository = sessionRepo;
            _userRepository = userRepository;
        }

        [Authorize(Roles = "HEAD,RESPONSIBLE")]
        public IActionResult Index()
        {
            IEnumerable<Session> allSessions = _sessionRepository.GetSessions().OrderBy(e => e.Eventdate).ThenBy(e => e.Starthour);

            return View(allSessions);
        }

        [Authorize(Roles = "HEAD,RESPONSIBLE")]
        public IActionResult OpenSession(int id)
        {
            Session session = _sessionRepository.GetById(id);
            ViewData["SessionInfo"] = session.Title + " op "+ session.Eventdate.ToShortDateString() + " om "+session.Starthour.Hours+ ":" + session.Starthour.Minutes;
            return View();
        }

        [Authorize(Roles = "HEAD,RESPONSIBLE")]
        [HttpPost, ActionName("OpenSession")]
        public IActionResult OpenSessionConfirmed(int id)
        {
            Session session = _sessionRepository.GetById(id);
            session.OpenSession();
            _sessionRepository.SaveChanges();
            return RedirectToAction(nameof(Index));

        }


        [Authorize(Roles = "HEAD,RESPONSIBLE")]
        public IActionResult SessionAddAttendees(int id)
        {
            Session session = _sessionRepository.GetById(id);
            TempData["namesession"] = session.Title;
            return View(new UserAttendViewModel() {Session = session });
        }

        
        [Authorize(Roles = "HEAD,RESPONSIBLE")]
        [HttpPost, ActionName("SessionAddAttendees")]
        public IActionResult SessionAddAttendeesConfirmed(int id, UserAttendViewModel insertedUser)
        {
            Session session = _sessionRepository.GetById(id);
            ItlabUser userAttend = _userRepository.GetById(insertedUser.Email);
            if(userAttend != null && session != null) {
                try 
                {
                    session.AddAttendeeUser(userAttend);
                    _sessionRepository.SaveChanges();
                    TempData["message"] = $"{userAttend.Username} is aangemeld";
                }
                catch(Exception e)
                {
                    TempData["error"] = e.Message;
                }
             
            }else
            {
                TempData["error"] = $"Er ging iets fout {insertedUser.Email} niet gevonden";
                
            }

            
          
            return View(new UserAttendViewModel() { Session = session });
        }
    }
    
}