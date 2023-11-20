using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    [InjectControls]
    public class EmployeeVacationListPage : PageBase
    {
        public EmployeeVacationListPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public Label TitleLabel { get; private set; }
        public Link ClaimsTab { get; private set; }
        public Link SalaryCalculatorTab { get; private set; }
        public Button CreateButton { get; private set; }
        public EmployeeClaimList ClaimList { get; private set; }

        public Label NoClaimsTextLabel { get; private set; }
        public PageFooter Footer { get; private set; }

        public void WaitLoaded(int? timeout = null)
        {
            TitleLabel.WaitPresence();
            CreateButton.WaitPresence();
        }
    }
}