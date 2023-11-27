using NUnit.Framework;
using VacationTests.Infrastructure;

namespace VacationTests.Tests.Navigation
{
    public class NavigationExercise1Tests : VacationTestBase
    {
        [Test]
        public void LoginPage_EmployeeButtonTest()
        {
            var enterPage = Navigation.OpenLoginPage();
            enterPage.LoginAsEmployeeButton.Present.Wait().EqualTo(true);
            enterPage.LoginAsEmployeeButton.Text.Wait().EqualTo("Я сотрудник");
        }
    }
}