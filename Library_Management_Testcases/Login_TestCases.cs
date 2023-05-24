using FluentAssertions;
using Library_Management_Application;
using Moq;


namespace Library_Management_Testcases
{
    public class Login_TestCases
    {
        [Fact]
        public void Login_Page_WhenSuccess_ReturnsTrue()
        {
            var loginmock = new Mock<LoginInterface>();
            loginmock.Setup(x => x.Login_Page()).Returns(true);
            var res = loginmock.Object.Login_Page();
            res.Should().BeTrue();
        }
        [Fact]
        public void Login_Page_WhenFailed_ReturnsFalse()
        {
            var loginmock = new Mock<LoginInterface>();
            loginmock.Setup(x => x.Login_Page()).Returns(false);
            var res = loginmock.Object.Login_Page();
            res.Should().Be(false);
        }
    }
}