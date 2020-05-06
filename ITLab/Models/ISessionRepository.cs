using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Models
{
    public interface ISessionRepository
    {
        Session GetById(int id);
        IList<Session> GetSessions();
        Session GetFirstComingSession();
        IList<Session> GetFirstComingSessions(int amount);

        void UpdateFinishedSessions();
        void SaveChanges();
    }
}
