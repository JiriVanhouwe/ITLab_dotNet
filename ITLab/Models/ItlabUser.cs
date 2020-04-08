using ITLab.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITLab.Models
{
    public partial class ItlabUser
    {
        public ItlabUser()
        {
            AttendeeUser = new HashSet<AttendeeUser>();
            Feedback = new HashSet<Feedback>();
            RegisterdUser = new HashSet<RegisterdUser>();
            Session = new HashSet<Session>();
        }

        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public UserStatus UserStatus { get; set; }
        public UserType UserType { get; set; }

 

        public virtual ICollection<AttendeeUser> AttendeeUser { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<RegisterdUser> RegisterdUser { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
}
