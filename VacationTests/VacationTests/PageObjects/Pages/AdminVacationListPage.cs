using Kontur.Selone.Selectors.Context;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Page;
using VacationTests.Infrastructure;
using VacationTests.PageObjects.Controls;

namespace VacationTests.PageObjects.Pages;

public class AdminVacationListPage : PageBase
{
    public AdminVacationListPage(IContextBy searchContext, IControlFactory controlFactory)
        : base(searchContext, controlFactory)
    {
        TitleLabel = controlFactory.CreateControl<Label>(WrappedDriver,"TitleLabel");
        ClaimsTab = controlFactory.CreateControl<Link>(WrappedDriver,"ClaimsTab");
        DownloadButton = controlFactory.CreateControl<Button>(WrappedDriver,"DownloadButton");
        Footer = controlFactory.CreateControl<PageFooter>(WrappedDriver,"Footer");
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