using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    public class EmployeeVacationListPage : PageBase
    {
        public EmployeeVacationListPage(IContextBy contextBy, ControlFactory controlFactory)
            : base(contextBy, controlFactory)
        {
            TitleLabel = FindByTid<Label>("TitleLabel");
            ClaimsTab = FindByTid<Link>("ClaimsTab");
            SalaryCalculatorTab = FindByTid<Link>("SalaryCalculatorTab");
            ClaimList = FindByTid<EmployeeClaimList>("ClaimList");
            Footer = FindByTid<PageFooter>("Footer");
            CreateButton = FindByTid<Button>("CreateButton");
        }

        public Label TitleLabel { get; set; }
        public Link ClaimsTab { get; set; }
        public Link SalaryCalculatorTab { get; }
        public EmployeeClaimList ClaimList { get; }
        public PageFooter Footer { get; set; }
        public Button CreateButton { get; }
    }
}