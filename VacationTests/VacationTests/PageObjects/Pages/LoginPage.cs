using Kontur.Selone.Selectors.Context;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Page;
using VacationTests.PageObjects.Controls;

// О наследовании https://ulearn.me/course/basicprogramming/Nasledovanie_ac2b8cb6-8d63-4b81-9083-eaa77ab0c89c
namespace VacationTests.PageObjects.Pages;

public class LoginPage : PageBase
{
    public LoginPage(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
        TitleLabel = controlFactory.CreateControl<Label>(WrappedDriver, "LoginTitleLabel");
        LoginAsEmployeeButton = controlFactory.CreateControl<Button>(WrappedDriver, "LoginAsEmployeeButton");
        Footer = controlFactory.CreateControl<PageFooter>(WrappedDriver, "Footer");
    }

    public Label TitleLabel { get; }
    public Button LoginAsEmployeeButton { get; }
    public PageFooter Footer { get; }

    public EmployeeVacationListPage LoginAsEmployee()
    {
        return LoginAsEmployeeButton.ClickAndOpen<EmployeeVacationListPage>();
    }
}