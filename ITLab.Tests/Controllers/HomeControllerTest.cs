using ITLab.Controllers;
using ITLab.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ITLab.Tests.Controllers
{
    public class HomeControllerTest
    {
        private HomeController _homeController;
        private Mock<ISessionRepository> _sessionRepository;
        private Mock<IUserRepository> _userRepository;

        public HomeControllerTest()
        {
            _sessionRepository = new Mock<ISessionRepository>();
            _userRepository = new Mock<IUserRepository>();

            _homeController = new HomeController(_userRepository.Object, _sessionRepository.Object);
        }

        [Fact]
        public void Index_WithSessions_ReturnsViewWithSessionsAndImagesInViewData()
        {
            var session = new Session() { Id = 21, Title = "Testen testen testen", Description = "We testen live een SessionController klasse uit", Nameguest = "test.test@test.com", Eventdate = new DateTime(), Starthour = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(2)), Endhour = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(4)) };
            var session2 = new Session() { Id = 22, Title = "Meer testen", Description = "We testen live een HomeController klasse uit", Nameguest = "test.test@test.com", Eventdate = new DateTime(), Starthour = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(3)), Endhour = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(5)) };
            var session3 = new Session() { Id = 23, Title = "Nog meer testen", Description = "We testen live een HomeController klasse uit", Nameguest = "test.test@test.com", Eventdate = new DateTime(), Starthour = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(5)), Endhour = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(6)) };
            _sessionRepository.Setup(m => m.GetFirstComingSessions(3)).Returns(new List<Session>() { session, session2});

            var result = Assert.IsType<ViewResult>(_homeController.Index());
            var model = Assert.IsAssignableFrom<IList<Session>>(result.Model);

            Assert.IsType<List<Image>>(result.ViewData["image"]);
        }
    }
}
