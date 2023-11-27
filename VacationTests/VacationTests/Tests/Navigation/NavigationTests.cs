using FluentAssertions;
using NUnit.Framework;

namespace VacationTests.Tests.Navigation
{
    public class NavigationTests : VacationTestBase
    {
        [Test]
        public void LoginPage_AdminButtonTest()
        {
            // Arrange
            var enterPage = Navigation.OpenLoginPage();
            enterPage.WaitLoaded();
            //Act
            var adminPage = enterPage.LoginAsAdmin();
            //Assert
            adminPage.IsAdminPage.Should().BeTrue();
        }
    }
}