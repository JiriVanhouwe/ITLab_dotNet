using ITLab.Models;
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
        private readonly ITLab_DBContext _context;

        public SessionRepository(ITLab_DBContext context)
        {
            this._context = context;
            this._sessions = context.Session;
        }

        public Session GetById(int id)
        {
            return _sessions
                        .Where(session => session.SessionCalendar.Startdate <= DateTime.Now && session.SessionCalendar.Enddate >= DateTime.Now)
                        .Include(session => session.RegisterdUser)
                        .Include(session => session.ClassroomClass)
                        .FirstOrDefault(session => session.Id == id);
        }

        public IList<Session> GetSessions()
        {
            return _sessions
                        .Where(session => session.SessionCalendar.Startdate <= DateTime.Now && session.SessionCalendar.Enddate >= DateTime.Now)
                        .Include(session => session.RegisterdUser)
                        .Include(session => session.ClassroomClass)
                        .ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
