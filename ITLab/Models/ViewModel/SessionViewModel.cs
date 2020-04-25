using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Models.ViewModel
{
    public class SessionViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string NameGuest { get; set; }
        public TimeSpan Starthour { get; set; }
        public TimeSpan Endhour { get; set; }
        public DateTime EventDate { get; set; }
        public int MaxAttendee { get; set; }
        public int RegisteredUsers { get; set; }
        public string ClassRoom { get; set; }

        public SessionViewModel(string title, string description, string nameGuest, TimeSpan startHour, TimeSpan endHour, DateTime eventDate, int maxAttendee, int registeredUsers, string classRoom)
        {
            Title = title;
            Description = description;
            NameGuest = nameGuest;
            Starthour = startHour;
            Endhour = endHour;
            EventDate = eventDate;
            MaxAttendee = maxAttendee;
            RegisteredUsers = registeredUsers;
            ClassRoom = classRoom;
        }

        public int SeatsAvailable()
        {
            return MaxAttendee - RegisteredUsers;
        }
    }
}
