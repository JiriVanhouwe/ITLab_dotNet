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
            Student = new ItlabUser
            {
                Firstname = "Brad",
                Lastname = "Pitt",
                Username = "brad.pitt@student.hogent.be",
                UserStatus = UserStatus.ACTIVE,
                UserType = UserType.USERITLAB
            };
            Hoofdverantwoordelijke = new ItlabUser();
            Verantwoordelijke = new ItlabUser();

            Users = new[] { Student };

            //SESSIONS
            //Session1
            Session1 = new Session
            {
                Title = "Coderen voor blinden",
                Id = 1,
                Eventdate = new DateTime(2020, 01, 01),
                Maxattendee = 100
            };

            Sessions = new[] { Session1 };

            //FEEDBACK
        }
    }
}
