using ITLab.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITLab.Tests.Data
{
    class DummyApplicationDbContext
    {
        public IEnumerable<ItlabUser> Users { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
        public IEnumerable<Feedback> Feedback { get; set; }

        public ItlabUser Student { get; set; }
        public ItlabUser Hoofdverantwoordelijke { get; set; }
        public ItlabUser Verantwoordelijke { get; set; }
        public Session Session1 { get; set; }


        public DummyApplicationDbContext()
        {
            //USERS
            //Gewone user
            Student = new ItlabUser();
            Student.Firstname = "Brad";
            Student.Lastname = "Pitt";
            Student.Username = "brad.pitt@student.hogent.be";
            Student.UserStatus = UserStatus.ACTIVE;
            Student.UserType = UserType.USERITLAB;
            Hoofdverantwoordelijke = new ItlabUser();
            Verantwoordelijke = new ItlabUser();

            Users = new[] { Student };

            //SESSIONS
            //Session1
            Session1 = new Session();
            Session1.Title = "Coderen voor blinden";
            Session1.Id = 1;
            Session1.Eventdate = new DateTime(2020, 01, 01);

            Sessions = new[] { Session1 };

            //FEEDBACK
        }
    }
}
