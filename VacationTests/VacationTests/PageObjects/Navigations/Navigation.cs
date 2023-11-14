using Kontur.Selone.Pages;
using OpenQA.Selenium;
using SeloneCore.Controls;
using SeloneCore.Page;
using VacationTests.PageObjects.Pages;

namespace VacationTests.PageObjects.Navigations;

public class Navigation
{
    private readonly IControlFactory controlFactory;
    private readonly IWebDriver webDriver;

    public Navigation(IWebDriver webDriver, IControlFactory controlFactory)
    {
        this.webDriver = webDriver;
        this.controlFactory = controlFactory;
    }

    public LoginPage OpenLoginPage() => OpenPage<LoginPage>(Urls.LoginPage);

    public EmployeeVacationListPage OpenEmployeeVacationListPage(string employeeId = "1")
    {
        var isCurrentPageIsEmployeePage = webDriver.Url.Contains("user");
        var page = OpenPage<EmployeeVacationListPage>(Urls.EmployeeVacationListPage(employeeId));

        // на фронте баг со страницей сотрудника
        // если находишься на странице сотрудника и меняешь цифру с id сотрудником в урле, то страница не обновляется
        // пока баг не исправлен, вставляем принудительный рефреш страницы
        if (isCurrentPageIsEmployeePage)
            page.Refresh();
        return page;
    }

    public TPageObject OpenPage<TPageObject>(string url) where TPageObject : PageBase
    {
        webDriver.Navigate().GoToUrl(url);
        webDriver.Navigate().Refresh();
        return controlFactory.CreatePage<TPageObject>(webDriver);
    }
}