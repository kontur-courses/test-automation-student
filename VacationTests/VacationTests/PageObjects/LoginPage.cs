using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

// О наследовании https://ulearn.me/course/basicprogramming/Nasledovanie_ac2b8cb6-8d63-4b81-9083-eaa77ab0c89c
namespace VacationTests.PageObjects
{
    public class LoginPage : PageBase
    {
        public LoginPage(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy, controlFactory)
        {
            TitleLabel = FindByTid<Label>("LoginTitleLabel");
            LoginAsEmployeeButton = FindByTid<Button>("LoginAsEmployeeButton");
            Footer = FindByTid<PageFooter>("Footer");
        }

        public Label TitleLabel { get; }
        public Button LoginAsEmployeeButton { get; }
        public PageFooter Footer { get; }

        public EmployeeVacationListPage LoginAsEmployee()
        {
            return LoginAsEmployeeButton.ClickAndOpen<EmployeeVacationListPage>();
        }
    }
}