using ITLab.Controllers;
using ITLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ITLab.Tests.Controllers
{
    public class SessionControllerTest
    {
        private SessionController _sessionController;
        private Mock<ISessionRepository> _sessionRepository;
        private Mock<IUserRepository> _userRepository;

        public SessionControllerTest()
        {
            _sessionRepository = new Mock<ISessionRepository>();
            _userRepository = new Mock<IUserRepository>();
            _sessionController = new SessionController(_sessionRepository.Object, _userRepository.Object);
        }

        #region Index
        [Fact]
        public void Index_NoUserLoggedInNoFeedback_GivesSessionBasedOnIdAndCorrectViewData()
        {
            var session = new Session() { Id = 21, Title = "Testen testen testen", Description = "We testen live een SessionController klasse uit", Nameguest = "test.test@test.com", Eventdate = new DateTime(), Starthour = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(2)), Endhour = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(4)) };
            _sessionRepository.Setup(m => m.GetById(21)).Returns(session);
            IUserRepository.LoggedInUser = null;

            var result = Assert.IsType<ViewResult>(_sessionController.Index(21));
            var model = Assert.IsType<Session>(result.Model);

            Assert.Equal(session.Id, model.Id);
            Assert.Equal(session.Title, model.Title);
            Assert.Equal(session.Description, model.Description);
            Assert.Equal(session.Eventdate, model.Eventdate);

            var userLoggedIn = Assert.IsType<bool>(result.ViewData["UserLoggedIn"]);
            Assert.False(userLoggedIn);
            var sessionIsFinished = Assert.IsType<bool>(result.ViewData["SessionIsFinished"]);
            Assert.False(sessionIsFinished);
            Assert.Null(result.ViewData["Feedback"]);

        }

        [Fact]
        public void IndexuserLoggedInAndFeeback_GivesSessionBasedOnIdAndCorrectViewData()
        {
            Session session = new Session() { Id = 21, Title = "Testen testen testen", Description = "We testen live een SessionController klasse uit", Nameguest = "test.test@test.com", Eventdate = new DateTime(), Starthour = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(2)), Endhour = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(4)) };
            ItlabUser user = new ItlabUser() { Firstname = "Mister", Lastname = "AdminMan" };
            _sessionRepository.Setup(m => m.GetById(21)).Returns(session);

            IUserRepository.LoggedInUser = user;
            session.AddFeedback(user, "Top sessie");

            var result = Assert.IsType<ViewResult>(_sessionController.Index(21));
            var model = Assert.IsType<Session>(result.Model);

            Assert.Equal(session.Id, model.Id);
            Assert.Equal(session.Title, model.Title);
            Assert.Equal(session.Description, model.Description);
            Assert.Equal(session.Eventdate, model.Eventdate);

            var userLoggedIn = Assert.IsType<bool>(result.ViewData["UserLoggedIn"]);
            Assert.True(userLoggedIn);
            var userAlreadyRegistered = Assert.IsType<bool>(result.ViewData["UserAlreadyRegistered"]);
            Assert.False(userAlreadyRegistered);
            var userAttended = Assert.IsType<bool>(result.ViewData["UserAttended"]);
            Assert.False(userAttended);

            var sessionIsFinished = Assert.IsType<bool>(result.ViewData["SessionIsFinished"]);
            Assert.False(sessionIsFinished);

            var feedback = result.ViewData["Feedback"];
            var feedbackList = Assert.IsAssignableFrom<ICollection<Feedback>>(feedback);
            Assert.Single(feedbackList);
        }

        #endregion

        [Fact]
        public void RegisterForSession_WrongId_ReturnsNotFound()
        {
            Assert.IsType<NotFoundResult>(_sessionController.RegisterForSession(25));
        }

        [Fact]
        public void RegisterForSession_NotYetSignedUp_UserSignedUpAndMessageInTempDataAndRedirectToIndex()
        {
            Session session = new Session() { Id = 21, Title = "Testen testen testen", Description = "We testen live een SessionController klasse uit", Nameguest = "test.test@test.com", Eventdate = new DateTime(), Starthour = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(2)), Endhour = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(4)) };
            _sessionRepository.Setup(m => m.GetById(21)).Returns(session);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var result = Assert.IsType<RedirectToActionResult>(_sessionController.RegisterForSession(21));
            Assert.Equal("index", result.ActionName);
            Assert.Equal("Je bent ingeschreven voor deze sessie.", tempData["message"]);

            _sessionRepository.Verify(m => m.SaveChanges(), Times.Once());
            _userRepository.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
