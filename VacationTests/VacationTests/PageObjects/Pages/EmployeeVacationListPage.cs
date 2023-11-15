using Kontur.Selone.Selectors.Context;
using SeloneCore;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Page;
using VacationTests.PageObjects.Controls;

namespace VacationTests.PageObjects.Pages;

public class EmployeeVacationListPage : PageBase
{
    public EmployeeVacationListPage(IContextBy contextBy, IPageObjectFactory pageObjectFactory)
        : base(contextBy, pageObjectFactory)
    {
        TitleLabel = pageObjectFactory.CreateControl<Label>(WrappedDriver, "TitleLabel");
        ClaimsTab = pageObjectFactory.CreateControl<Link>(WrappedDriver, "ClaimsTab");
        SalaryCalculatorTab = pageObjectFactory.CreateControl<Link>(WrappedDriver, "SalaryCalculatorTab");
        ClaimList = pageObjectFactory.CreateControl<EmployeeClaimList>(WrappedDriver, "ClaimList");
        Footer = pageObjectFactory.CreateControl<PageFooter>(WrappedDriver, "Footer");
        CreateButton = pageObjectFactory.CreateControl<Button>(WrappedDriver, "CreateButton");
    }

    public Label TitleLabel { get; set; }
    public Link ClaimsTab { get; set; }
    public Link SalaryCalculatorTab { get; }
    public EmployeeClaimList ClaimList { get; }
    public PageFooter Footer { get; set; }
    public Button CreateButton { get; }
}