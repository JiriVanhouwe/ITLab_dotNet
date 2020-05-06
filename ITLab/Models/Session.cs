using ITLab.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public string CardDescription
        {
            get {
                string NewDescription = Description;
                
                if (Description.Length > 103 )
                {
                    NewDescription = Description.Substring(0, 100) + "...";
                }

                return NewDescription;
            }
        }

        [NotMapped]
        public string CardTitle
        {
            get
            {
                string NewTitle = Title;

                if (Title.Length > 25)
                {
                    NewTitle = Title.Substring(0, 22) + "...";
                }

                return NewTitle;
            }
        }

        public Session()
        {
            AttendeeUser = new HashSet<AttendeeUser>();
            Feedback = new HashSet<Feedback>();
            RegisterdUser = new HashSet<RegisterdUser>();
        }

        public void openSession()
        {
            Stateenum = State.OPEN;
            //hier nog state aanpassen want nu enkel enum?
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

        public void addAttendeeUser(ItlabUser itlabUser)
        {
            if (!RegisterdUser.Any(e => e.UserUsernameNavigation.Equals(itlabUser)))
            {
                AddRegisteredUser(itlabUser);
            }
            if (!AttendeeUser.Any(e => e.UserUsernameNavigation.Equals(itlabUser)))
            {
                AttendeeUser.Add(new AttendeeUser(this, itlabUser));
            }
            else
                throw new ArgumentException("user is al aanwezig");
            
        }
    }
}
