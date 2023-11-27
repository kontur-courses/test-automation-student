using Kontur.Selone.Pages;
using Kontur.Selone.Properties;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using VacationTests.Claims;
using VacationTests.Helpers;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;
using Claim = VacationTests.Claims.Claim;

namespace VacationTests.Tests.AdministratorPage
{
    public class AdminListTests : VacationTestBase
    {
        //Проверить пустой список
        [Test]
        public void AdminList_ShouldDisplayCorrectEmptyList()
        {
            var page = Navigation.OpenAdminVacationListPage();
            page.TitleLabel.Text.Wait().EqualTo("Список отпусков");
            page.ClaimsTab.Visible.Wait().EqualTo(true);
            page.ClaimsTab.Text.Wait().EqualTo("🌴 Заявления на отпуск");
            page.NoClaimsTextLabel.Text.Wait().EqualTo("Нет заявлений");

            page.Footer.KnowEnvironmentLink.Visible.Wait().EqualTo(true);
            page.Footer.KnowEnvironmentLink.Text.Wait().EqualTo("Узнать окружение");
            page.Footer.OurFooterLink.Visible.Wait().EqualTo(true);
            page.Footer.OurFooterLink.Text.Wait().EqualTo("Наш футер");
        }

        [Test]
        public void AdminList_CorrectClaimsList()
        {
            var page = Navigation.OpenEmployeeVacationListPage();
            var claim1 = Claim.CreateDefault() with { UserId = "2" };
            var claim2 = Claim.CreateDefault() with { UserId = "1" };
            var createClaim = new Helper();
            createClaim.CreateClaimFromUI(page, claim1);
            createClaim.CreateClaimFromUI(page, claim2);

            var adminPage = Navigation.OpenAdminVacationListPage();
            adminPage.ClaimList.Present.Wait().EqualTo(true);
            adminPage.ClaimList.Items.Count.Wait().EqualTo(2);
        }

        [Test]
        public void AdminList_CorrectClaimsListWithData()
        {
            var adminPage = Navigation.OpenAdminVacationListPage();
            adminPage.NoClaimsTextLabel.Text.Wait().EqualTo("Нет заявлений");

            var claim1 = Claim.CreateDefault() with { UserId = "2" };
            var claim2 = Claim.CreateDefault() with { UserId = "1" };

            ClaimStorage.Add(claim1);
            ClaimStorage.Add(claim2);

            adminPage.Refresh();
            adminPage.ClaimList.Present.Wait().EqualTo(true);
            adminPage.ClaimList.Items.Count.Wait().EqualTo(2);

            var claims = new[]
            {
                ("Заявление "+claim1.Id, claim1.StartDate.ToString("dd.MM.yyyy") + " - " + claim1.EndDate.ToString("dd.MM.yyyy"), claim1.Status.GetDescription()),
                ("Заявление "+claim2.Id, claim2.StartDate.ToString("dd.MM.yyyy") + " - " + claim2.EndDate.ToString("dd.MM.yyyy"), claim2.Status.GetDescription())
            };

            adminPage.NoClaimsTextLabel.Present.Wait().EqualTo(false);
            adminPage.ClaimList.Items
                .Select(claim => Props.Create(claim.TitleLink.Text, claim.PeriodLabel.Text, claim.StatusLabel.Text))
                .Wait().EquivalentTo(claims);
        }

        [Test]
        public void AdminList_CorrectViewForButtons()
        {
            var adminPage = Navigation.OpenAdminVacationListPage();

            var claim1 = Claim.CreateDefault() with { Status = ClaimStatus.NonHandled };
            var claim2 = Claim.CreateDefault() with { Status = ClaimStatus.Accepted };
            var claim3 = Claim.CreateDefault() with { Status = ClaimStatus.Rejected };

            ClaimStorage.Add(claim1);
            ClaimStorage.Add(claim2);
            ClaimStorage.Add(claim3);

            adminPage.Refresh();
            adminPage.ClaimList.Present.Wait().EqualTo(true);
            adminPage.ClaimList.Items.Count.Wait().EqualTo(3);

            var claims = new[]
            {
                ("Заявление "+claim1.Id, claim1.StartDate.ToString("dd.MM.yyyy") + " - " + claim1.EndDate.ToString("dd.MM.yyyy"), claim1.Status.GetDescription(), true, true),
                ("Заявление "+claim2.Id, claim2.StartDate.ToString("dd.MM.yyyy") + " - " + claim2.EndDate.ToString("dd.MM.yyyy"), claim2.Status.GetDescription(), false, false),
                ("Заявление "+claim3.Id, claim2.StartDate.ToString("dd.MM.yyyy") + " - " + claim2.EndDate.ToString("dd.MM.yyyy"), claim3.Status.GetDescription(), false, false)
            };

            adminPage.ClaimList.Items
                .Select(claim => Props.Create(claim.TitleLink.Text, claim.PeriodLabel.Text, claim.StatusLabel.Text, claim.AcceptButton.Visible, claim.RejectButton.Visible))
                .Wait().EquivalentTo(claims);
        }

        static IEnumerable<TestCaseData> DivideCases()
        {
            yield return new TestCaseData(ClaimStatus.Accepted, new Func<PageElements.AdminClaimItem, Button>(item => item.AcceptButton)).SetName("AdminList_CorrectButtonWork Accepted");
            yield return new TestCaseData(ClaimStatus.Rejected, new Func<PageElements.AdminClaimItem, Button>(item => item.RejectButton)).SetName("AdminList_CorrectButtonWork Rejected");
            yield return new TestCaseData(ClaimStatus.Accepted, new Func<PageElements.AdminClaimItem, Button>(item =>
            {
                var lightbox = item.TitleLink.ClickAndOpen<ClaimLightbox>();
                return lightbox.Footer.AcceptButton;
            })).SetName("AdminList_CorrectButtonWork Lightbox - Accepted");

            yield return new TestCaseData(ClaimStatus.Rejected, new Func<PageElements.AdminClaimItem, Button>(item =>
            {
                var lightbox = item.TitleLink.ClickAndOpen<ClaimLightbox>();
                return lightbox.Footer.RejectButton;
            })).SetName("AdminList_CorrectButtonWork Lightbox - Rejected");
        }

        [TestCaseSource(nameof(DivideCases))]
        public void AdminList_CorrectButtonWork(ClaimStatus claimStatus, Func<PageElements.AdminClaimItem, Button> getButton)
        {
            var adminPage = Navigation.OpenAdminVacationListPage();
            var claim = Claim.CreateDefault();
            ClaimStorage.Add(claim);
            adminPage.Refresh();

            var items = adminPage.ClaimList.Items;

            getButton(items.Single(x => x.TitleLink.Text.Get() == "Заявление " + claim.Id)).Click();
            var claims = new[]
            {
                ("Заявление "+claim.Id, claimStatus.GetDescription(), false, false),
            };

            adminPage.ClaimList.Items
                .Select(claim => Props.Create(claim.TitleLink.Text, claim.StatusLabel.Text, claim.AcceptButton.Visible, claim.RejectButton.Visible))
                .Wait().EquivalentTo(claims);
        }

        [Test]
        public void AdminList_CheckOrder()
        {
            var adminPage = Navigation.OpenAdminVacationListPage();
            adminPage.NoClaimsTextLabel.Text.Wait().EqualTo("Нет заявлений");

            var claim1 = Claim.CreateDefault() with { UserId = "2" };
            var claim2 = Claim.CreateDefault() with { UserId = "1" };

            ClaimStorage.Add(claim1);
            ClaimStorage.Add(claim2);

            adminPage.Refresh();
            adminPage.ClaimList.Present.Wait().EqualTo(true);
            adminPage.ClaimList.Items.Count.Wait().EqualTo(2);

            var claims = new[]
            {
                ("Заявление "+claim1.Id),
                ("Заявление "+claim2.Id),
            };

            adminPage.ClaimList.Items.Select(x=> x.TitleLink.Text)
                .Wait().EqualTo(claims);
        }

        [TearDown]
        protected override void TearDown()
        {
            ClaimStorage.ClearClaims();
            base.TearDown();
        }
    }
}