﻿using ITLab.Controllers;
using ITLab.Models;
using ITLab.Models.ViewModel;
using ITLab.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ITLab.Tests.Controllers
{
    public class FeedbackControllerTest
    {
        private readonly FeedbackController _feedbackController;
        private readonly Mock<ISessionRepository> _sessionRepo;
        private readonly Mock<IUserRepository> _userRepo;
        private readonly DummyApplicationDbContext _dummyContext;


       public FeedbackControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _sessionRepo = new Mock<ISessionRepository>();
            _userRepo = new Mock<IUserRepository>();
            _feedbackController = new FeedbackController(_sessionRepo.Object, _userRepo.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }


        [Fact]
        public void Index_StoresSessionIdInViewData_ReturnsView()
        {
            _sessionRepo.Setup(s => s.GetById(1)).Returns(_dummyContext.Session1);

            var result = Assert.IsType<ViewResult>(_feedbackController.Index(1));
            var model = Assert.IsType<FeedbackViewModel>(result.Model);
            var viewdata = Assert.IsType<int>(result.ViewData["sessionId"]);

            Assert.Equal("Coderen voor blinden", model.Title);
            Assert.Equal(1, viewdata);
        }
        
        [Fact]
        public void IndexPost_UpdatesSessionWithFeedback_RedirectsToActionIndex()
        {
            _userRepo.Setup(u => u.GetById("brad.pitt@student.hogent.be")).Returns(_dummyContext.Student);
            _sessionRepo.Setup(s => s.GetById(1)).Returns(_dummyContext.Session1);
            var fvm = new FeedbackViewModel()
            {
                Title = _dummyContext.Session1.Title,
                Feedback = "Zeer goede sessie"
            };
            _dummyContext.Session1.AddFeedback(_dummyContext.Student, fvm.Feedback);

            var result = Assert.IsType<RedirectToActionResult>(_feedbackController.Index(1, fvm));
            Assert.Equal(1, _dummyContext.Session1.Feedback.Count);
            Assert.Equal("Index", result.ActionName);
           // _sessionRepo.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}