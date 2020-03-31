using ITLab.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ITLab.Tests.Models
{
    public class SessionCalendarTest
    {
        private readonly DateTime _validStartDate;
        private readonly DateTime _validEndDate;
        private readonly IEnumerable<Session> _session;
        private readonly string _validID;
        private readonly SessionCalendar _validsessionCalendar;
        public SessionCalendarTest()
        {
            _validStartDate = DateTime.Parse("2019-09-01");
            _validEndDate = DateTime.Parse("2020-08-30");
            _session =  new List<Session>();
            _validID = ("20192020");
            _validsessionCalendar = new SessionCalendar(_validID, _session, _validStartDate, _validEndDate);

        }

        #region Constructor
        [Fact]
        public void SessionCalender_Valid()
        {
            _validsessionCalendar.Id.Equals(_validID);
            _validsessionCalendar.StartDate.Equals(_validStartDate);
            _validsessionCalendar.EndDate.Equals(_validEndDate);

        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("20192019")]
        [InlineData("2010321")]
        [InlineData("201032123")]
        public void SessionCalender_WrongId_ThrowsArgumentException(string wrongId)
        {
            Assert.Throws<ArgumentException>(() => new SessionCalendar(wrongId, _session, _validStartDate, _validEndDate));
        }

       
        #endregion

    }


}
