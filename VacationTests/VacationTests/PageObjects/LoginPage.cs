using Kontur.Selone.Extensions;
using Kontur.Selone.Waiting;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

// О наследовании https://ulearn.me/course/basicprogramming/Nasledovanie_ac2b8cb6-8d63-4b81-9083-eaa77ab0c89c
namespace VacationTests.PageObjects
{
    public class LoginPage : PageBase, ILoadable
    {
        public LoginPage(IWebDriver webDriver, ControlFactory controlFactory) : base(webDriver)
        {
            // Можно упростить написание для частых поисков, и создать свой метод WithTid(), чтобы опустить Css(),
            // этот метод будет вызывать Css().WithTid("..."))
            TitleLabel = controlFactory.CreateControl<Label>(webDriver.Search(x => x.WithTid("TitleLabel")));
            LoginAsEmployeeButton =
                controlFactory.CreateControl<Button>(webDriver.Search(x => x.WithTid("LoginAsEmployeeButton")));
            LoginAsAdminButton =
                controlFactory.CreateControl<Button>(webDriver.Search(x => x.WithTid("LoginAsAdminButton")));
            Footer = controlFactory.CreateControl<PageFooter>(webDriver.Search(x => x.WithTid("Footer")));
        }

        public Label TitleLabel { get; }
        public Button LoginAsEmployeeButton { get; }

        public Button LoginAsAdminButton { get; }
        public PageFooter Footer { get; }

        public EmployeeVacationListPage LoginAsEmployee()
        {
            return LoginAsEmployeeButton.ClickAndOpen<EmployeeVacationListPage>();
        }

        public AdminVacationListPage LoginAsAdmin()
        {
            return LoginAsAdminButton.ClickAndOpen<AdminVacationListPage>();
        }

        public void WaitLoaded(int? timeout = null)
        {
            LoginAsEmployeeButton.WaitPresence();
            LoginAsAdminButton.WaitPresence();
        }
    }
}