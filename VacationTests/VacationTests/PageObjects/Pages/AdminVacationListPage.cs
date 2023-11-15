using Kontur.Selone.Selectors.Context;
using SeloneCore;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Page;
using VacationTests.Infrastructure;
using VacationTests.PageObjects.Controls;

namespace VacationTests.PageObjects.Pages;

public class AdminVacationListPage : PageBase
{
    public AdminVacationListPage(IContextBy searchContext, IPageObjectFactory pageObjectFactory)
        : base(searchContext, pageObjectFactory)
    {
        TitleLabel = pageObjectFactory.CreateControl<Label>(WrappedDriver,"TitleLabel");
        ClaimsTab = pageObjectFactory.CreateControl<Link>(WrappedDriver,"ClaimsTab");
        DownloadButton = pageObjectFactory.CreateControl<Button>(WrappedDriver,"DownloadButton");
        Footer = pageObjectFactory.CreateControl<PageFooter>(WrappedDriver,"Footer");
    }

    public Label TitleLabel { get; }
    public Link ClaimsTab { get; }
    public Button DownloadButton { get; }
    public PageFooter Footer { get; }

    public bool IsAdminPage
    {
        get
        {
            var employeeVacationListPage = new PageObjectFactory().CreatePage<EmployeeVacationListPage>(WrappedDriver);
            return !(employeeVacationListPage.SalaryCalculatorTab.Visible.Get()
                     && employeeVacationListPage.CreateButton.Visible.Get());
        }
    }
}