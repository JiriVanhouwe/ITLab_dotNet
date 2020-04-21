using ITLab.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITLab.Models
{
    public partial class ItlabUser : IdentityUser
    {
        public ItlabUser()
        {
            AttendeeUser = new HashSet<AttendeeUser>();
            Feedback = new HashSet<Feedback>();
            RegisterdUser = new HashSet<RegisterdUser>();
            Session = new HashSet<Session>();
        }

        public override string UserName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public override string PasswordHash { get; set; }
        public UserStatus UserStatus { get; set; }
        public UserType UserType { get; set; }

 

        public virtual ICollection<AttendeeUser> AttendeeUser { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<RegisterdUser> RegisterdUser { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
}
