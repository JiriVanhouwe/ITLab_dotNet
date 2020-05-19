using ITLab.Controllers;
using ITLab.Models;
using ITLab.Tests.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ITLab.Tests.Controllers
{
    public class CalendarControllerTest
    {
        private readonly CalendarController _calendarController;
        private readonly Mock<ISessionRepository> _sessionRepo;
        private readonly DummyApplicationDbContext _dummyContext;

        public CalendarControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _sessionRepo = new Mock<ISessionRepository>();
            _calendarController = new CalendarController(_sessionRepo.Object);
        }

        [Fact]
        public void Index_ReturnsFinishedAndOpenSessions_ReturnsView()
        {
            _sessionRepo.Setup(s => s.GetFinshedAndOpenSessions()).Returns(_dummyContext.Sessions.ToList());

            var result = Assert.IsType<ViewResult>(_calendarController.Index());
            var model = Assert.IsType<List<Session>>(result.Model);

            Assert.Equal(model.Count, _dummyContext.Sessions.Count());
        }
    }
}
