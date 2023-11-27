using Kontur.Selone.Properties;
using NUnit.Framework;
using System;
using System.Linq;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.EmployeePage
{
    public class EmployeeVacationsListUiTests: VacationTestBase
    {
        //[Test]
        //Задание 4.1
        //public void CreateClaimFromUI()
        //{
        //    //тестовые данные
        //    Claim AChildClaim = new ClaimBuilder()
        //        .WithType(ClaimType.Child)
        //        .WithChildAgeInMonths(new Random().Next(1, 101))
        //        .WithStatus(ClaimStatus.NonHandled)
        //        .Build();
        //    var age = AChildClaim.ChildAgeInMonths / 12;
        //    var startDate = AChildClaim.StartDate.ToString("dd.MM.yyyy");
        //    var endDate = AChildClaim.EndDate.ToString("dd.MM.yyyy");
        //    var claimTitle = "Заявление 1";

        //    var page = Navigation.OpenEmployeeVacationListPage();
        //    page.ClaimList.Items.Count.Wait().EqualTo(0);

        //    var claimCreationPage = page.CreateButton.ClickAndOpen<ClaimCreationPage>();
        //    claimCreationPage.ClaimTypeSelect.SelectValueByText(AChildClaim.Type.GetDescription());
        //    claimCreationPage.ChildAgeInput.ClearAndInputText(age.ToString());
        //    claimCreationPage.ClaimStartDatePicker.SetValue(startDate);
        //    claimCreationPage.ClaimEndDatePicker.SetValue(endDate);
        //    claimCreationPage.DirectorFioCombobox.SelectValue(AChildClaim.Director.Name);
        //    claimCreationPage.DirectorFioCombobox.WaitValue(AChildClaim.Director.Name);

        //    page = claimCreationPage.SendButton.ClickAndOpen<EmployeeVacationListPage>();
        //    page.WaitLoaded();

        //    page.ClaimList.Items.Count.Wait().EqualTo(1);
        //    var claim = page.ClaimList.Items.Wait().Single(
        //        x => x.TitleLink.Text, Is.EqualTo(claimTitle));
        //    claim.PeriodLabel.Text.Wait().EqualTo(startDate + " - " + endDate);
        //    claim.StatusLabel.Text.Wait().EqualTo(AChildClaim.Status.GetDescription());
        //}

        //Задание 4.2
        public void CreateClaimFromUI(
            EmployeeVacationListPage page, Claim claim, string startDate, string endDate)
        {
            var claimCreationPage = page.CreateButton.ClickAndOpen<ClaimCreationPage>();
            claimCreationPage.ClaimTypeSelect.SelectValueByText(claim.Type.GetDescription());

            if (claim.ChildAgeInMonths != null)
            {
                var age = claim.ChildAgeInMonths / 12;
                claimCreationPage.ChildAgeInput.ClearAndInputText(age.ToString());
            }

            claimCreationPage.ClaimStartDatePicker.SetValue(startDate);
            claimCreationPage.ClaimEndDatePicker.SetValue(endDate);
            claimCreationPage.DirectorFioCombobox.SelectValue(claim.Director.Name);
            claimCreationPage.DirectorFioCombobox.WaitValue(claim.Director.Name);

            page = claimCreationPage.SendButton.ClickAndOpen<EmployeeVacationListPage>();
            page.WaitLoaded();
        }

        [Test]
        public void CreateVacations_ShouldAddItemsToClaimsList()
        {
            Claim AChildClaim = CreateAchildClaim();

            var startDate = AChildClaim.StartDate.ToString("dd.MM.yyyy");
            var endDate = AChildClaim.EndDate.ToString("dd.MM.yyyy");

            var page = Navigation.OpenEmployeeVacationListPage(new Random().Next(1, 101).ToString());
            page.ClaimList.Items.Count.Wait().EqualTo(0);

            CreateClaimFromUI(page, AChildClaim, startDate, endDate);
            CreateClaimFromUI(page, AChildClaim, startDate, endDate);

            page.ClaimList.Items.Count.Wait().EqualTo(2);
        }

        private static Claim CreateAchildClaim()
        {
            //тестовые данные
            Claim AChildClaim = new ClaimBuilder()
                .WithType(ClaimType.Child)
                //поменяла на 12 месяцев, потому как при вводе 0 лет фронт думает, что поле не заполнено
                .WithChildAgeInMonths(new Random().Next(12, 101))
                .WithStatus(ClaimStatus.NonHandled)
                .Build();
            return AChildClaim;
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitles_InRightOrder()
        {
            Claim AChildClaim = CreateAchildClaim();

            var startDate = AChildClaim.StartDate.ToString("dd.MM.yyyy");
            var endDate = AChildClaim.EndDate.ToString("dd.MM.yyyy");

            var page = Navigation.OpenEmployeeVacationListPage(new Random().Next(1, 101).ToString());
            page.ClaimList.Items.Count.Wait().EqualTo(0);

            CreateClaimFromUI(page, AChildClaim, startDate, endDate);
            CreateClaimFromUI(page, AChildClaim, startDate, endDate);
            CreateClaimFromUI(page, AChildClaim, startDate, endDate);

            page.ClaimList.Items.Select(x => x.TitleLink.Text)
                .Wait().EqualTo(new[] { "Заявление 1", "Заявление 2", "Заявление 3" });
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitleAndStatus_IgnoringOrder()
        {
            Claim AChildClaim = CreateAchildClaim();

            var startDateFirstClaim = DateTime.Now.Date.AddDays(7).ToString("dd.MM.yyyy");
            var endDateFirstClaim = DateTime.Now.Date.AddDays(15).ToString("dd.MM.yyyy");

            var startDateSecondClaim = DateTime.Now.Date.AddDays(10).ToString("dd.MM.yyyy");
            var endDateSecondClaim = DateTime.Now.Date.AddDays(20).ToString("dd.MM.yyyy");

            var page = Navigation.OpenEmployeeVacationListPage(new Random().Next(1, 101).ToString());
            page.ClaimList.Items.Count.Wait().EqualTo(0);

            CreateClaimFromUI(page, AChildClaim, startDateFirstClaim, endDateFirstClaim);
            CreateClaimFromUI(page, AChildClaim, startDateSecondClaim, endDateSecondClaim);

            page.ClaimList.Items.Count.Wait().EqualTo(2);

            var expected = new[]
                {
                    ("Заявление 2", startDateSecondClaim + " - " + endDateSecondClaim, true),
                    ("Заявление 1", startDateFirstClaim + " - " + endDateFirstClaim, true)
                };

            page.ClaimList.Items
                .Select(claim => Props.Create(claim.TitleLink.Text, claim.PeriodLabel.Text, claim.StatusLabel.Visible))
                .Wait().EquivalentTo(expected);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightPeriodForItem()
        {
            Claim AChildClaim = CreateAchildClaim();

            var startDateFirstClaim = DateTime.Now.Date.AddDays(7).ToString("dd.MM.yyyy");
            var endDateFirstClaim = DateTime.Now.Date.AddDays(15).ToString("dd.MM.yyyy");

            var startDateSecondClaim = DateTime.Now.Date.AddDays(10).ToString("dd.MM.yyyy");
            var endDateSecondClaim = DateTime.Now.Date.AddDays(20).ToString("dd.MM.yyyy");

            var page = Navigation.OpenEmployeeVacationListPage(new Random().Next(1, 101).ToString());
            page.ClaimList.Items.Count.Wait().EqualTo(0);

            CreateClaimFromUI(page, AChildClaim, startDateFirstClaim, endDateFirstClaim);
            CreateClaimFromUI(page, AChildClaim, startDateSecondClaim, endDateSecondClaim);

            page.ClaimList.Items.Count.Wait().EqualTo(2);

            var claim = page.ClaimList.Items.Wait().Single(
                x => x.TitleLink.Text, Is.EqualTo("Заявление 2"));

            claim.PeriodLabel.Text.Wait().EqualTo(startDateSecondClaim + " - " + endDateSecondClaim);
            claim.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
        }

        [Test]
        public void PeriodTest()
        {
            var startDateFirstClaim = DateTime.Now.Date.AddDays(7);
            var endDateFirstClaim = DateTime.Now.Date.AddDays(15);

            //тестовые данные
            Claim AChildClaim = new ClaimBuilder()
                .WithType(ClaimType.Child)
                //поменяла на 12 месяцев, потому как при вводе 0 лет фронт думает, что поле не заполнено
                .WithChildAgeInMonths(new Random().Next(12, 101))
                .WithStatus(ClaimStatus.NonHandled)
                .WithPeriod(startDateFirstClaim, endDateFirstClaim)
                .Build();

            var page = Navigation.OpenEmployeeVacationListPage(new Random().Next(1, 101).ToString());
            page.ClaimList.Items.Count.Wait().EqualTo(0);

            CreateClaimFromUI(page, AChildClaim, startDateFirstClaim.ToString("dd.MM.yyyy"), endDateFirstClaim.ToString("dd.MM.yyyy"));

            page.ClaimList.Items.Count.Wait().EqualTo(1);
        }
    }
}