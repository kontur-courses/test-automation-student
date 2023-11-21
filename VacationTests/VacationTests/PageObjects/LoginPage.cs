using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    [InjectControls]
    public class LoginPage : PageBase
    {
        public LoginPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public Label TitleLabel { get; private set; }
        public Button LoginAsEmployeeButton { get; private set;}
        public Button LoginAsAdminButton { get; private set;}
        public PageFooter Footer { get; private set;}

        public EmployeeVacationListPage LoginAsEmployee()
        {
            return LoginAsEmployeeButton.ClickAndOpen<EmployeeVacationListPage>();
        }

        public AdminVacationListPage LoginAsAdmin()
        {
            return LoginAsAdminButton.ClickAndOpen<AdminVacationListPage>();
        }

        public void WaitLoaded(int? timeout = null)
        {
            LoginAsEmployeeButton.Present.Wait().EqualTo(true, timeout);
            LoginAsAdminButton.Present.Wait().EqualTo(true, timeout);
        }
    }
}