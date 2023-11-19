using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    [InjectControls]
    public class AdminVacationListPage : PageBase
    {
        public AdminVacationListPage(IWebDriver webDriver, ControlFactory controlFactory) : base(webDriver)
        {
            TitleLabel = controlFactory.CreateControl<Label>(webDriver.Search(x => x.WithTid("TitleLabel")));
            ClaimsTab = controlFactory.CreateControl<Link>(webDriver.Search(x => x.WithTid("ClaimsTab")));
            ClaimList = controlFactory.CreateControl<AdminClaimList>(webDriver.Search(x => x.WithTid("ClaimList")));
            DownloadButton = controlFactory.CreateControl<Button>(webDriver.Search(x => x.WithTid("DownloadButton")));
            Footer = controlFactory.CreateControl<PageFooter>(webDriver.Search(x => x.WithTid("Footer")));
        }

        public AdminClaimList ClaimList { get; set; }

        public Label TitleLabel { get; }
        public Link ClaimsTab { get; }
        public Button DownloadButton { get; }
        public PageFooter Footer { get; }

        public bool IsAdminPage
        {
            get
            {
                var employeeVacationListPage = new ControlFactory().CreatePage<EmployeeVacationListPage>(WrappedDriver);
                return !(employeeVacationListPage.SalaryCalculatorTab.Visible.Get()
                         && employeeVacationListPage.CreateButton.Visible.Get());
            }
        }

        public void WaitLoaded(int? timeout = null)
        {
            ClaimsTab.WaitPresence(timeout);
            TitleLabel.WaitPresence(timeout);
        }
    }
}