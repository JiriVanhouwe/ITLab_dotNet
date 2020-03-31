using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
