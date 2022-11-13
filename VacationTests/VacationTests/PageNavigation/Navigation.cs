using OpenQA.Selenium;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.PageNavigation
{
    public class Navigation
    {
        private readonly ControlFactory controlFactory;
        private readonly IWebDriver webDriver;

        public Navigation(IWebDriver webDriver, ControlFactory controlFactory)
        {
            this.webDriver = webDriver;
            this.controlFactory = controlFactory;
        }

        public LoginPage OpenLoginPage()
        {
            return OpenPage<LoginPage>(Urls.LoginPage);
        }

        public EmployeeVacationListPage OpenEmployeeVacationList(string employeeId = "1")
        {
            return OpenPage<EmployeeVacationListPage>(Urls.EmployeeVacationList(employeeId));
        }

        public TPageObject OpenPage<TPageObject>(string url)
            where TPageObject : PageBase
        {
            webDriver.Navigate().GoToUrl(url);
            return controlFactory.CreatePage<TPageObject>(webDriver);
        }
    }
}