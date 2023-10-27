using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    public class AdminVacationListPage : PageBase
    {
        public AdminVacationListPage(IContextBy searchContext, ControlFactory controlFactory)
            : base(searchContext, controlFactory)
        {
            TitleLabel = FindByTid<Label>("TitleLabel");
            ClaimsTab = FindByTid<Link>("ClaimsTab");
            DownloadButton = FindByTid<Button>("DownloadButton");
            Footer = FindByTid<PageFooter>("Footer");
        }

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