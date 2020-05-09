using ITLab.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ITLab.Tests.Models
{
    public class SessionTest
    {
        private ItlabUser _dummyUser;
        private ItlabUser _dummyUserBlocked;
        private ItlabUser _dummyUserNonactive;
        private Classroom _dummyClassroom;
        Session _session;

        public SessionTest()
        {
            _dummyClassroom = new Classroom() { Classid = "classroom", Campus = Campus.GENT, Maxseats = 50, Roomcategory = RoomCategory.ITLAB };
            _dummyUser = new ItlabUser() { Firstname = "firstname", Lastname = "lastname", Username = "firstname.lastname@hogent.be", UserType = UserType.RESPONSIBLE, UserStatus = UserStatus.ACTIVE, Password = "password" };
            _dummyUserBlocked = new ItlabUser() { Firstname = "firstname", Lastname = "lastname", Username = "firstname.lastname@hogent.be", UserType = UserType.RESPONSIBLE, UserStatus = UserStatus.BLOCKED, Password = "password" };
            _dummyUserNonactive = new ItlabUser() { Firstname = "firstname", Lastname = "lastname", Username = "firstname.lastname@hogent.be", UserType = UserType.RESPONSIBLE, UserStatus = UserStatus.NONACTIVE, Password = "password" };

            TimeSpan ts = new TimeSpan(15, 50, 0);
            TimeSpan startTime = ts;
            TimeSpan endTime = ts.Add(new TimeSpan(0, 50, 0));
            _session = new Session() { Id = 1, Endhour = endTime, Starthour = startTime, Maxattendee = 10, ClassroomClass = _dummyClassroom };
        }




        [Fact]
        public void addValidUserToRegisterd()
        {
            _session.AddAttendeeUser(_dummyUser);
            bool flag = false;
            {
                foreach(RegisterdUser r in _session.RegisterdUser)
                {
                    if(r.UserUsernameNavigation == _dummyUser)
                    {
                        flag = true;
                    }
                }
            }

            Assert.True(flag);
        }

        [Fact]
        public void addNonactiveUserToRegisterd() {
           Assert.Throws<ArgumentException>( () =>_session.AddRegisteredUser(_dummyUserNonactive));
        }

        [Fact]
        public void addBlockedUserToRegisterd()
        {
            Assert.Throws<ArgumentException>(() => _session.AddRegisteredUser(_dummyUserBlocked));
        }


        [Fact]
        public void addValidUserToAtendee()
        {
            _session.AddAttendeeUser(_dummyUser);
            bool flag = false;
            {
                foreach (AttendeeUser r in _session.AttendeeUser)
                {
                    if (r.UserUsernameNavigation == _dummyUser)
                    {
                        flag = true;
                    }
                }
            }

            Assert.True(flag);
        }

        [Fact]
        public void addNonactiveUserToAtendee()
        {
            Assert.Throws<ArgumentException>(() => _session.AddAttendeeUser(_dummyUserNonactive));
        }

        [Fact]
        public void addBlockedUserToAtendee()
        {
            Assert.Throws<ArgumentException>(() => _session.AddAttendeeUser(_dummyUserBlocked));
        }

    }

}
