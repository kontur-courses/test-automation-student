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

        [Test]
        [Category("Flaky")]
        public void NavigationToAdminListPage_Success()
        {
            var page = Navigation.OpenAdminVacationListPage();
            page.ClaimList.Items.Count.Should().Be(0);
        }
    }
}