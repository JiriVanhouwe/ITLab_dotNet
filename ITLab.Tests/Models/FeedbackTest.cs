using ITLab.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ITLab.Tests.Models
{
    public class FeedbackTest
    {
        /*private readonly ItlabUser author;

        public FeedbackTest()
        {
            author = new ItlabUser("Frank", "Deboosere", "frank.deboosere@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "MooiWeer");
        }

        #region CONSTRUCTOR
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("      ")]
        public void Constructor_WrongData_ThrowsException(string content)
        {
            Assert.Throws<ArgumentException>(() => new Feedback(author, content));
        }

        [Fact]
        public void Constructor_NoAuthor_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Feedback(null, "Een fraaie boodschap"));
        }

        [Theory]
        [InlineData("Wat een prachtige sessie.")]
        [InlineData("23049283048")]
        [InlineData("ù$^)àç")]
        [InlineData("J4 d1t w4s g3w3ld1g.")]
        [InlineData("Zolang                er maar               tekst is.")]
        public void Constructor_CorrectData_CreatesFeedback(string content)
        {
            Feedback feedback = new Feedback(author, content);
            Assert.Equal(feedback.Author, author);
            Assert.Equal(feedback.Content, content);
        } 
        #endregion
       */
    }
}
