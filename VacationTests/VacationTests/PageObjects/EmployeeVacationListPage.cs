using Kontur.Selone.Extensions;
using OpenQA.Selenium;
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
            ClaimList = controlFactory.CreateControl<EmployeeClaimList>(webDriver.Search(x => x.WithTid("ClaimList")));
            //добавляла в задании 4.1
            //NoClaimsTextLabel = controlFactory.CreateControl<Label>(webDriver.Search(x => x.WithTid("NoClaimsTextLabel")));
            Footer = controlFactory.CreateControl<PageFooter>(webDriver.Search(x => x.WithTid("Footer")));
            
        }



        public Label TitleLabel { get; }
        public Link ClaimsTab { get; }
        public Link SalaryCalculatorTab { get; }
        public Button CreateButton { get; }
        public EmployeeClaimList ClaimList { get; }
        
        //добавляла в задании 4.1
        //public Label NoClaimsTextLabel { get; }
        public PageFooter Footer { get; }

        public void WaitLoaded(int? timeout = null)
        {
            TitleLabel.WaitPresence();
            CreateButton.WaitPresence();
        }
    }
}