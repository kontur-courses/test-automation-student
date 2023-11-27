using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Css;
using Kontur.Selone.Selectors.XPath;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V108.CacheStorage;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    [InjectControls]
    public class EmployeeVacationListPage : PageBase
    {
        public EmployeeVacationListPage(IWebDriver webDriver, ControlFactory controlFactory) : base(webDriver)
        {
            TitleLabel = controlFactory.CreateControl<Label>(webDriver.Search(x => x.WithTid("TitleLabel")));
            ClaimsTab = controlFactory.CreateControl<Link>(webDriver.Search(x => x.WithTid("ClaimsTab")));
            SalaryCalculatorTab =
                controlFactory.CreateControl<Link>(webDriver.Search(x => x.WithTid("SalaryCalculatorTab")));
            CreateButton = controlFactory.CreateControl<Button>(webDriver.Search(x => x.WithTid("CreateButton")));
            ClaimList = controlFactory.CreateControl<EmployeeClaimList>(webDriver.Search(x => x.Css().WithTid("ClaimList")));
            Footer = controlFactory.CreateControl<PageFooter>(webDriver.Search(x => x.WithTid("Footer")));
            NoClaimsTextLabel = controlFactory.CreateControl<Label>(webDriver.Search(x => x.WithTid("NoClaimsTextLabel")));
        }

        public Label NoClaimsTextLabel { get; }
        public Label TitleLabel { get; }
        public Link ClaimsTab { get; }
        public Link SalaryCalculatorTab { get; }
        public Button CreateButton { get; }
        public EmployeeClaimList ClaimList { get; }
        public PageFooter Footer { get; }

        public void WaitLoaded(int? timeout = null)
        {
            TitleLabel.Present.Wait().EqualTo(true, timeout);
            ClaimList.Present.Wait().EqualTo(true, timeout);
        }
    }
}