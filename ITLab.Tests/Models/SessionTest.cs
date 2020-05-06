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
        private Classroom _dummyClassroom;
        private DateTime _startTime;

        public SessionTest()
        {
            //_dummyClassroom = new Classroom() { Classid = "classroom", Campus = Campus.GENT, Maxseats = "50",Roomcategory = ClassroomCategory.ITLAB };
            _dummyUser = new ItlabUser() { Firstname = "firstname",  Lastname ="lastname",  Username = "firstname.lastname@hogent.be",UserType =  UserType.RESPONSIBLE, UserStatus = UserStatus.ACTIVE, Password ="password" };
            _startTime = DateTime.Now;
            TimeSpan ts = new TimeSpan(15, 50, 0);
            _startTime = _startTime.Date + ts;
        }

        
        public void Initialize()
        {

        }

        #region CONSTRUCTOR




        #endregion
    }
}
