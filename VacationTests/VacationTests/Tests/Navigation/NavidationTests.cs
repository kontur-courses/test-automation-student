using NUnit.Framework;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.Tests.Navigation
{
    public class NavigationTests : VacationTestBase
    {
        [Category("Flaky")]
        [Test]
        public void AdminButton_PassToAdminPage_Success()
        {
            var enterPage = Navigation.OpenLoginPage();
            enterPage.WaitLoaded(); //Arrange

            var adminPage = enterPage.LoginAsAdmin();//Act

            Assert.IsTrue(adminPage.IsAdminPage); //Assert
            adminPage.ClaimList.WaitAbsence();
        }
    }
}