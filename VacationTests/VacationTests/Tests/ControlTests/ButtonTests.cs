using Kontur.Selone.Pages;
using NUnit.Framework;
using SeloneCore.Controls;
using SeloneCore.Props;
using VacationTests.Claims;
using VacationTests.PageObjects.Pages;

namespace VacationTests.Tests.ControlTests;

public class ButtonTests : VacationTestBase
{
    [Test]
    public void WaitExample()
    {
        // todo для курсанта: заменить на var page = Navigation.OpenAdminVacationList(); после доабвления метода (Задание 2)
        var page = Navigation.OpenPage<AdminVacationListPage>("https://ronzhina.gitlab-pages.kontur.host/for-course/#/admin");
        // todo для курсанта: после создания рекорда (Задание 6) заменить создание дефолтного отпуска на создание через рекорд
        // ClaimStorage.Add(new[] {Claim.CreateDefault(), Claim.CreateDefault(), Claim.CreateDefault()});
        ClaimStorage.Add(new[] {CreateDefaultClaim(), CreateDefaultClaim(), CreateDefaultClaim()});
        page.Refresh();

        page.DownloadButton.Text.Wait().EqualTo<string, string>("Скачать их отпуска");
        page.DownloadButton.Text.Wait().That<string, string>(Contains.Substring("Скачать"));

        page.DownloadButton.Present.Wait().EqualTo(true);
        page.DownloadButton.Disabled.Wait().EqualTo(true);

        // Сокращенный вариант
        page.DownloadButton.WaitPresence();
        page.DownloadButton.WaitDisabled();
    }

    [Test]
    public void OperationExample()
    {
        // Метод ClickAndOpen()
        var page = Navigation.OpenEmployeeVacationListPage();
        var claimPage = page.CreateButton.ClickAndOpen<ClaimCreationPage>();

        // Метод Click()
        claimPage.SendButton.Present.Wait().EqualTo(true);
        claimPage.SendButton.Click();
        claimPage.DirectorFioCombobox.HasError.Wait().EqualTo(true);
    }
        
    // todo для курсанта: после создания рекорда (Задание 6) удалить код ниже
    private Claim CreateDefaultClaim()
    {
        var random = new Random();
        var randomClaimId = random.Next(1, 101).ToString();
        var defaultDirector = new Director(14, "Бублик Владимир Кузьмич", "Директор департамента");
        return new Claim(randomClaimId, ClaimType.Study, ClaimStatus.Accepted, defaultDirector,
            DateTime.Now.Date.AddDays(14), DateTime.Now.Date.AddDays(14 + 7), null,"55", true);
    }
}