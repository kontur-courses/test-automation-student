using NUnit.Framework;
using SeloneCore.Controls;
using SeloneCore.Props;
using VacationTests.PageObjects.Pages;

namespace VacationTests.Tests.ControlTests;

public class SidePageTests : VacationTestBase
{
    [Test]
    public void GoToLoginPage_OpenSidePage_CheckText_Close()
    {
        var page = Navigation.OpenLoginPage();
        var sidePage = page.Footer.OurFooterLink.ClickAndOpen<InfoSidePage>();
        sidePage.HeaderLabel.Text.Wait().EqualTo<string, string>("Чтение вредит");
        sidePage.BodyLabel.Text.Wait().That<string, string>(Contains.Substring("Многие знания — многие скорби"));
        sidePage.BodyLabel.Text.Wait().That<string, string>(Contains.Substring("Лучше жить реальностью"));
        sidePage.AgreeButton.Text.Wait().EqualTo<string, string>("Точно так");
        sidePage.NotAgreeButton.Text.Wait().EqualTo<string, string>("Но это неточно");
        sidePage.CloseButton.ClickAndOpen<LoginPage>();
        sidePage.HeaderLabel.Visible.Wait().EqualTo(false);
    }
}