﻿using ITLab.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Data.Repositories
{
    public class SessionRepository : ISessionRepository
    {

        private readonly DbSet<Session> _sessions;
        private readonly DbSet<SessionMedia> _sessionMedia;
        private readonly DbSet<Image> _images;
        private readonly ITLab_DBContext _context;


        public SessionRepository(ITLab_DBContext context)
        {
            _context = context;
            _sessions = context.Session;
            _images = context.Image;
            _sessionMedia = context.SessionMedia;
        }

        public Session GetById(int id)
        {
            return _sessions
                        .Where(session => session.SessionCalendar.Startdate <= DateTime.Now && session.SessionCalendar.Enddate >= DateTime.Now)
                        .Include(session => session.Feedback)
                        .Include(session => session.AttendeeUser).ThenInclude(e => e.UserUsernameNavigation)
                        .Include(session => session.RegisterdUser).ThenInclude(e => e.UserUsernameNavigation)
                        .Include(session => session.ClassroomClass)
                        .FirstOrDefault(session => session.Id == id);
        }

        public Session GetFirstComingSession()
        {
            return _sessions.Where(session =>  session.Stateenum == State.OPEN)
                            .Where(s => s.Eventdate >= DateTime.Now).OrderBy(s => s.Eventdate).FirstOrDefault();
        }

        public IList<Session> GetFirstComingSessions(int amount)
        {
            List<Session> upcomingSessions = _sessions.Where(session => session.Stateenum == State.OPEN)
                                                      .Where(s => s.Eventdate >= DateTime.Now).OrderBy(s => s.Eventdate).ToList();
            if(amount <= upcomingSessions.Count)
            {
                return upcomingSessions.GetRange(0, amount).ToList();
            }
            return upcomingSessions;
        }

        public IList<Session> GetSessions()
        {
            return _sessions
                        .Where(session => session.SessionCalendar.Startdate <= DateTime.Now && session.SessionCalendar.Enddate >= DateTime.Now)
                        .Include(session => session.RegisterdUser)
                        .Include(session => session.AttendeeUser)
                        .Include(session => session.ClassroomClass)
                        .ToList();
        }

        public void UpdateFinishedSessions()
        {
            List<Session> sessionsOfThePast = _sessions.Where(session => session.Eventdate < DateTime.Now && session.Stateenum != State.FINISHED).ToList();
            if (sessionsOfThePast != null)
            {
                sessionsOfThePast.ForEach(el => Console.WriteLine(el.Stateenum));
            }
            else Console.WriteLine("NIETS");

            foreach (Session s in sessionsOfThePast)
            {
                s.Stateenum = State.FINISHED;
            }

            _context.SaveChanges();
        }

        public Image GetImage(int id)
        {
            List<SessionMedia> list = _sessionMedia.Where(i => i.SessionId == id).ToList();

            return list.Select(el => el.Media).Select(el => _images.SingleOrDefault(el2 => el2.Imagekey == el)).FirstOrDefault();
        }

        public List<Image> GetImages(int id)
        {
            List<SessionMedia> list = _sessionMedia.Where(i => i.SessionId == id).ToList();
            return list.Select(el => el.Media).Select(el => _images.SingleOrDefault(el2 => el2.Imagekey == el)).ToList();

        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IList<Session> GetFinshedAndOpenSessions()
        {
            return _sessions
                       .Where(session => session.SessionCalendar.Startdate <= DateTime.Now && session.SessionCalendar.Enddate >= DateTime.Now)
                       .Where(session => session.Stateenum == State.FINISHED || session.Stateenum == State.OPEN)
                       .Include(session => session.RegisterdUser)
                       .Include(session => session.AttendeeUser)
                       .Include(session => session.ClassroomClass)
                       .ToList();
        }
    }
}
