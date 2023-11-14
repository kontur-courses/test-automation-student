using Kontur.Selone.Selectors.Context;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Page;
using VacationTests.PageObjects.Controls;

namespace VacationTests.PageObjects.Pages;

public class EmployeeVacationListPage : PageBase
{
    public EmployeeVacationListPage(IContextBy contextBy, IControlFactory controlFactory)
        : base(contextBy, controlFactory)
    {
        TitleLabel = controlFactory.CreateControl<Label>(WrappedDriver, "TitleLabel");
        ClaimsTab = controlFactory.CreateControl<Link>(WrappedDriver, "ClaimsTab");
        SalaryCalculatorTab = controlFactory.CreateControl<Link>(WrappedDriver, "SalaryCalculatorTab");
        ClaimList = controlFactory.CreateControl<EmployeeClaimList>(WrappedDriver, "ClaimList");
        Footer = controlFactory.CreateControl<PageFooter>(WrappedDriver, "Footer");
        CreateButton = controlFactory.CreateControl<Button>(WrappedDriver, "CreateButton");
    }

    public Label TitleLabel { get; set; }
    public Link ClaimsTab { get; set; }
    public Link SalaryCalculatorTab { get; }
    public EmployeeClaimList ClaimList { get; }
    public PageFooter Footer { get; set; }
    public Button CreateButton { get; }
}