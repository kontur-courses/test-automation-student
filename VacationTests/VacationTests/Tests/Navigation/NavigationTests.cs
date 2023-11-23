using System;
using NUnit.Framework;
using VacationTests.Infrastructure;

namespace VacationTests.Tests.Navigation
{
    public class NavigationTests : VacationTestBase
    {
        [Test]
        public void NavigationToAdminListPage_Success()
        {
            var enterPage = Navigation.OpenLoginPage();
            enterPage.WaitLoaded();
            var adminVacationListPage = enterPage.LoginAsAdmin();
            Assert.IsTrue(adminVacationListPage.IsAdminPage);
            adminVacationListPage.ClaimList.ClaimItems.Count.Wait().EqualTo(0);
            // Добавил проверку четности секунд для падения теста
            var currentSecond = int.Parse(DateTime.Now.Second.ToString());
            Assert.IsTrue(currentSecond % 2 == 0);
        }
    }
}