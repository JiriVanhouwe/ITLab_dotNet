using ITLab.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ITLab.Tests.Models
{
    public class UserTest
    {

        #region CONSTRUCTOR
        [Theory]
        [InlineData("donald", "troemp", "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "AmericaIsGreat")]
        [InlineData("donald", "Troemp", "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "AmericaIsGreat")]
        [InlineData("DonAld", "troemp", "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "AmericaIsGreat")]
        [InlineData("Donald", "TroEmp", "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "AmericaIsGreat")]
        [InlineData("DONALD", "TROEMP", "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "AmericaIsGreat")]
        [InlineData("Donald", "Troemp", "doNAld.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "AmericaIsGreat")]
        [InlineData("Donald", "Troemp", "donald.TROemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "AmericaIsGreat")]
        [InlineData("Donald", "Van Troemp", "donald.vantroemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "AmericaIsGreat")]
        [InlineData("Donald", "Troemp", "donald.troemp@hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "AmericaIsGreat")]
        public void User_CorrectData_CreatesUser(string firstName, string lastName, string userName, UserType userType, UserStatus userStatus, string password)
        {
            User user = new User(firstName, lastName, userName, userType, userStatus, password);
            Assert.Equal(user.FirstName, firstName);
            Assert.Equal(user.LastName, lastName);
            Assert.Equal(user.UserName, userName);
            Assert.Equal(user.UserType, userType);
            Assert.Equal(user.UserStatus, userStatus);
            Assert.Equal(user.Password, password);
        }

		[Theory]
		//fout in voornaam
		[InlineData("Don4ld", "Troemp", "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Don)-)ld", "Troemp", "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("1234", "Troemp", "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("$-)à", "Troemp", "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("   ", "Troemp", "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData(null, "Troemp", "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		//fout in familienaam
		[InlineData("Donald", "Tr09mp", "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Tro09$-)mp", "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "8976", "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "!ùù=:", "don4ld.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "   ", "don4ld.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", null, "donald.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		//fout in emailadres
		[InlineData("Donald", "Troemp", "barack.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "donald.obama@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "3mepd.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "234.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "$ù-))à.troemp@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "donald.098@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "donald.µ$ù=:@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "donald.09ù^$µ@student.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "donald.troemp@blablabla", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "donald.troempstudent.hogent.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "donald.troemp@gmail.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "donald.troemp@123.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "donald.troemp@$^^ùd.be", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "donald.troemp@student.hogent", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "    ", UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", null, UserType.USERITLAB, UserStatus.ACTIVE, "123")]
		//fout in een van de enums
		[InlineData("Donald", "Troemp", "donald.troemp@hogent.be", null, UserStatus.ACTIVE, "123")]
		[InlineData("Donald", "Troemp", "donald.troemp@student.hogent.be", UserType.USERITLAB, null, "123")]
        public void User_WrongData_ThrowsException(string firstName, string lastName, string userName, UserType userType, UserStatus userStatus, string password)
        {
			Assert.Throws<ArgumentException>(() => new User(firstName, lastName, userName, userType, userStatus, password));
        }
        #endregion

    }
}
