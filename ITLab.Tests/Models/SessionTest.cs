using ITLab.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ITLab.Tests.Models
{
    public class SessionTest
    {
        private User _dummyUser;
        private Classroom _dummyClassroom;
        private DateTime _startTime;

        public SessionTest()
        {
            _dummyClassroom = new Classroom("classroom", Campus.GENT, 50, ClassroomCategory.ITLAB);
            _dummyUser = new User("firstname", "lastname", "firstname.lastname@hogent.be", UserType.RESPONSIBLE, UserStatus.ACTIVE, "password");
            _startTime = DateTime.Now;
            TimeSpan ts = new TimeSpan(15, 50, 0);
            _startTime = _startTime.Date + ts;
        }

        
        public void Initialize()
        {

        }

        #region CONSTRUCTOR




        [Theory]
        [InlineData("A new session", "some text", 2, 2, 10, "guestspeaker")]
        [InlineData("A session about ...", "some text", 50, 30, 20, "guestspeaker")]
        [InlineData("S", "some text", 5, 1, 30, "guestspeaker")]
        public void Session_CorrectData_CreateSession(string title, string description, int addDays, int addMinutes, int maxAttendees, string guestName)
        {
            DateTime startDateTime = _startTime.AddDays(addDays);
            DateTime endDateTime = _startTime.AddDays(addDays).AddMinutes(addMinutes);

            Session session = new Session(title, description, startDateTime, endDateTime, maxAttendees, guestName);
            Assert.Equal(session.Title, title);
            Assert.Equal(session.Description, description);
            Assert.Equal(session.StartDateTime, startDateTime);
            Assert.Equal(session.EndDateTime, endDateTime);
            Assert.Equal(session.MaxAttendees, maxAttendees);
            Assert.Equal(session.Guestname, guestName);
        }

        [Theory]
        [InlineData("   ", "some text", 2, 0, 2, 2, 51, "guestspeaker")] // invalid title
        [InlineData("A new session on an interesting topic", "some text", 2, 0, 2, 2, 51, "guestspeaker")] // invalid maxAttendees (greater than maxSeats)
        [InlineData("A new session on an interesting topic", "some text", 30, 0, 30, 29, 30, "guestspeaker")] // invalid enddate (duration too short)
        [InlineData("A new session on an interesting topic", "some text", 30, 0, 30, -10, 30, "guestspeaker")] // invalid enddate (negative duration)
        [InlineData("A new session on an interesting topic", "some text", 30, -2, 30, 0, 51, "guestspeaker")] // invalid startdate (after enddate)
        [InlineData("A new session on an interesting topic", "some text", 0, -2, 0, 0, 51, "guestspeaker")] // invalid startdate (in the past)
        [InlineData("A new session on an interesting topic", "some text", 0, 12, 0, 14, 51, "guestspeaker")] // invalid startdate (less than 1 day in advance)
        [InlineData("A new session on an interesting topic", "some text", 30, 0, 30, 2, 0, "guestspeaker")] // invalid maxAttendees (zero)
        [InlineData("A new session on an interesting topic", "some text", 30, 0, 30, 2, -1, "guestspeaker")] // invalid maxAttendees (negative)
        public void Session_WrongData_ThrowsException(string title, string description, int addStartDays, int addStartMinutes, int addEndDays, int addEndMinutes, int maxAttendees, string guestName)
        {
            DateTime startDateTime = _startTime.AddDays(addStartDays).AddMinutes(addStartMinutes);
            DateTime endDateTime = _startTime.AddDays(addEndDays).AddMinutes(addEndMinutes);

            Assert.Throws<ArgumentException>(() => new Session(title, description, startDateTime, endDateTime, maxAttendees, guestName));
        }

        #endregion
    }
}
