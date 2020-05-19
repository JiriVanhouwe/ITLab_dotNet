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

        public void OpenSession()
        {
            Stateenum = State.OPEN;
            //hier nog state aanpassen want nu enkel enum?
        }

        public void AddRegisteredUser(ItlabUser user)
        {
            if(user.UserStatus == UserStatus.ACTIVE)
            {
                RegisterdUser.Add(new RegisterdUser(this, user));
            }
            else
            {
                throw new ArgumentException("Je account staat op nonactive contacteer de itlab verantwoordlijke");
            }
            
        }

        internal void RemoveAttendeeUser(ItlabUser userAttend)
        {
            AttendeeUser.Remove(AttendeeUser.First(e => e.UserUsernameNavigation.Equals(userAttend)));
        }

        public void RemoveRegisteredUser(ItlabUser user)
        {

            RegisterdUser.Remove(RegisterdUser.First(e => e.UserUsername == user.Username));
        }

        public bool IsUserRegistered(string userName)
        {
            return RegisterdUser.Any(u => u.UserUsername == userName);
        }

        public bool hasUserAttended(string userName)
        {
            return AttendeeUser.Any(e => e.UserUsername.Equals(userName));
        }


        public int SeatsAvailable()
        {
            return Maxattendee - RegisterdUser.Count;
        }

        public void AddFeedback(ItlabUser user, string text)
        {
            if (user.UserStatus == UserStatus.ACTIVE)
            {
                Feedback.Add(new Feedback(user, text));
            }
            else
            {
                throw new ArgumentException("Je account staat op nonactive contacteer de itlab verantwoordlijke");
            }
            ;
        }

        public void AddAttendeeUser(ItlabUser itlabUser)
        {
            if (!RegisterdUser.Any(e => e.UserUsernameNavigation.Equals(itlabUser)))
            {
                if(RegisterdUser.Count() < Maxattendee)
                    AddRegisteredUser(itlabUser);
                else
                    throw new ArgumentException("user niet ingeschreven max aantal plaatsen bereikt");
            }
            if (!AttendeeUser.Any(e => e.UserUsernameNavigation.Equals(itlabUser)))
            {
                AttendeeUser.Add(new AttendeeUser(this, itlabUser));
            }
            else
                throw new ArgumentException("user is al aanwezig");
            
        }

        public bool IsUserAtendee(string userId)
        {
            return  AttendeeUser.Any(e => e.UserUsername.Split('@')[0].Replace(".", string.Empty).Equals(userId, StringComparison.InvariantCultureIgnoreCase));
            
        }

        public bool IsUserRegisterd(string userId)
        {
            return RegisterdUser.Any(e => e.UserUsername.Split('@')[0].Replace(".", string.Empty).Equals(userId, StringComparison.InvariantCultureIgnoreCase));
        }

        public DateTime GiveDeadlineForFeedback()
        {
            return Eventdate.AddDays(14);
        }
    }
}
