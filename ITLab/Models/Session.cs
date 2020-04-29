using ITLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITLab.Models
{
    public partial class Session
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public TimeSpan Endhour { get; set; }
        public DateTime Eventdate { get; set; }
        public int Maxattendee { get; set; }
        public string Nameguest { get; set; }
        public TimeSpan Starthour { get; set; }
        public State Stateenum { get; set; }
        public string Title { get; set; }
        public string Videourl { get; set; }
        public string ClassroomClassid { get; set; }
        public string HostUsername { get; set; }
        public int? SessionCalendarId { get; set; }

        public virtual Classroom ClassroomClass { get; set; }
        public virtual ItlabUser HostUsernameNavigation { get; set; }
        public virtual SessionCalendar SessionCalendar { get; set; }
        public virtual ICollection<AttendeeUser> AttendeeUser { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<RegisterdUser> RegisterdUser { get; set; }

        public Session()
        {
            AttendeeUser = new HashSet<AttendeeUser>();
            Feedback = new HashSet<Feedback>();
            RegisterdUser = new HashSet<RegisterdUser>();
        }

        public void AddRegisteredUser(ItlabUser user)
        {
            RegisterdUser.Add(new RegisterdUser(this, user));
        }

        public void RemoveRegisteredUser(ItlabUser user)
        {
            RegisterdUser.Remove(RegisterdUser.First(e => e.UserUsername == user.Username));
        }

        public bool IsUserRegistered(string userName)
        {
            if (RegisterdUser.Any(u => u.UserUsername == userName))
                return true;
            else
                return false;
        }


        public int SeatsAvailable()
        {
            return Maxattendee - RegisterdUser.Count;
        }

        public void AddFeedback(ItlabUser user, string text)
        {
            Feedback.Add(new Feedback(user, text));
        }
    }
}
