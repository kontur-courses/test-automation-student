using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Css;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    [InjectControls]
    public class AdminVacationListPage : PageBase
    {
        private ControlFactory controlFactory;
        public AdminVacationListPage(IWebDriver webDriver, ControlFactory controlFactory) : base(webDriver)
        {
            this.controlFactory = controlFactory;
            TitleLabel = controlFactory.CreateControl<Label>(webDriver.Search(x => x.WithTid("TitleLabel")));
            ClaimsTab = controlFactory.CreateControl<Link>(webDriver.Search(x => x.WithTid("ClaimsTab")));
            DownloadButton = controlFactory.CreateControl<Button>(webDriver.Search(x => x.WithTid("DownloadButton")));
            Footer = controlFactory.CreateControl<PageFooter>(webDriver.Search(x => x.WithTid("Footer")));
            NoClaimsTextLabel = controlFactory.CreateControl<Label>(webDriver.Search(x => x.WithTid("NoClaimsTextLabel")));
            ClaimList = controlFactory.CreateControl<AdminClaimList>(webDriver.Search(x => x.Css().WithTid("ClaimList")));
        }

        public AdminClaimList ClaimList { get; set; }
        public Label NoClaimsTextLabel { get; }
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
    }
}