using ITLab.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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

        public string Username 
        {
            get { return Username; }
            set { if(true) { Username = value; } }
        }
        public string Firstname
        {
            get { return Firstname; }
            set { if(Regex.IsMatch(value, @"^[a-zA-Z]+$")) { Firstname = value; } }
        }
        public string Lastname
        {
            get { return Lastname; }
            set { if (Regex.IsMatch(value, @"^[a-zA-Z]+$")) { Lastname = value; } }
        }
        public string Password { get; set; }
        public UserStatus UserStatus
        {
            get { return UserStatus; }
            set { }
        }
        public UserType UserType
        {
            get { return UserType; }
            set { }
        }

        public virtual ICollection<AttendeeUser> AttendeeUser { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<RegisterdUser> RegisterdUser { get; set; }
        public virtual ICollection<Session> Session { get; set; }

    }
}
