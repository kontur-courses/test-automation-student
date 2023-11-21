using FluentAssertions;
using NUnit.Framework;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.Tests.Navigation
{
    public class NavigationTests: VacationTestBase

    {
        [Test]
        public void LoginAsAdmin_CheckAdminVacationListPageIsOpen()
        {
            var enterPage = Navigation.OpenLoginPage();
            enterPage.LoginAsAdminButton.Present.Wait().EqualTo(true);
            
            var vacationPage = enterPage.LoginAsAdmin();
            vacationPage.IsAdminPage.Should().Be(true);
        }
    }
}