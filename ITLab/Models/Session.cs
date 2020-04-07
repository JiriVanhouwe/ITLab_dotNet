using ITLab.Models;
using System;
using System.Collections.Generic;

namespace TestDatabase.Models
{
    public partial class Session
    {
        public Session()
        {
            AttendeeUser = new HashSet<AttendeeUser>();
            Feedback = new HashSet<Feedback>();
            RegisterdUser = new HashSet<RegisterdUser>();
        }

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
    }

   
}

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDatabase.Models;

namespace ITLab.Models
{
    public enum State
    {
        FINISHED, OPEN, CLOSED
    }


    public class Session
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Guestname { get; set; }
        public Classroom Classroom { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int MaxAttendees { get; set; }
        public IEnumerable<int> Media { get; set; }
        public string VideoUrl { get; set; }
        public IEnumerable<User> RegisteredUsers { get; set; }
        public IEnumerable<User> Attendees { get; set; }
        public User Host { get; set; }
        public IEnumerable<Feedback> Feedbacks { get; set; }
        protected State StateName { get; set; }
        protected SessionState State { get; set; }

        public Session(string title, string description, DateTime startDateTime, DateTime endDateTime, int maxAttendees, string guestName)
        {
            Title = title;
            Description = description;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            MaxAttendees = maxAttendees;
            Guestname = guestName;
        }
    }
}
*/
