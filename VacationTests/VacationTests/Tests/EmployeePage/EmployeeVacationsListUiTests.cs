using System;
using System.Linq;
using System.Threading;
using Kontur.Selone.Properties;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.EmployeePage
{
    public class EmployeeVacationsListUiTests : VacationTestBase
    {
        private const string DateFormat = "dd.MM.yyyy";

        [Test]
        public void CreateVacations_ShouldAddItemsToClaimsList()
        {
            var vacationPage = Navigation.OpenEmployeeVacationListPage();
            vacationPage.WaitLoaded();
            vacationPage.ClaimList.Items.Count.Wait().EqualTo(0);
            
            CreateClaimFromUI(vacationPage);
            CreateClaimFromUI(vacationPage);
            
            vacationPage.ClaimList.Items.Count.Wait().EqualTo(2);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitles_InRightOrder()
        {
            var vacationPage = Navigation.OpenEmployeeVacationListPage("2");
            vacationPage.WaitLoaded();
            vacationPage.ClaimList.Items.Count.Wait().EqualTo(0);
            
            CreateClaimFromUI(vacationPage);
            CreateClaimFromUI(vacationPage);
            CreateClaimFromUI(vacationPage);

            vacationPage.ClaimList.Items.Count.Wait().EqualTo(3);
            vacationPage.ClaimList.Items
                .Select(element => element.TitleLink.Text)
                .Wait()
                .EqualTo(new[]
                {
                    "Заявление 1",
                    "Заявление 2",
                    "Заявление 3"
                });
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitleAndStatus_IgnoringOrder()
        {
            var vacationPage = Navigation.OpenEmployeeVacationListPage("3");
            vacationPage.WaitLoaded();
            vacationPage.ClaimList.Items.Count.Wait().EqualTo(0);

            var firstClaimStartDate = GetStartDateFromNow(10);
            var firstClaimEndDate = GetEndDateFromNow(24);
            var secondClaimStartDate = GetStartDateFromNow(50);
            var secondClaimEndDate = GetEndDateFromNow(60);

            CreateClaimFromUI(vacationPage, firstClaimStartDate, firstClaimEndDate);
            CreateClaimFromUI(vacationPage, secondClaimStartDate, secondClaimEndDate);

            var expected = new[]
            {
                (
                    "Заявление 1", GetPeriod(firstClaimStartDate, firstClaimEndDate),
                    ClaimStatus.NonHandled.GetDescription()
                ),
                (
                    "Заявление 2", GetPeriod(secondClaimStartDate, secondClaimEndDate),
                    ClaimStatus.NonHandled.GetDescription()
                )
            };

            vacationPage.ClaimList.Items.Select(element => Props.Create(
                element.TitleLink.Text,
                element.PeriodLabel.Text,
                element.StatusLabel.Text)
            ).Wait().EquivalentTo(expected);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightPeriodForItem()
        {
            var vacationPage = Navigation.OpenEmployeeVacationListPage("4");
            vacationPage.WaitLoaded();
            vacationPage.ClaimList.Items.Count.Wait().EqualTo(0);

            var secondClaimStartDate = GetStartDateFromNow(50);
            var secondClaimEndDate = GetEndDateFromNow(64);
            CreateClaimFromUI(vacationPage);
            CreateClaimFromUI(vacationPage, secondClaimStartDate, secondClaimEndDate);

            var chaim = vacationPage.ClaimList.Items.Wait().Single(
                element => element.TitleLink.Text,
                Is.EqualTo("Заявление 2")
            );

            var expectedPeriod = GetPeriod(secondClaimStartDate, secondClaimEndDate);
            chaim.PeriodLabel.Text.Wait().EqualTo(expectedPeriod);
        }

        private void CreateClaimFromUI(
            EmployeeVacationListPage page,
            DateTime? startDate = null,
            DateTime? endDate = null,
            ClaimType type = ClaimType.Paid,
            string directorFio = "Бублик Владимир Кузьмич",
            int childrenAge = 7)
        {
            var createVacationPage = page.CreateButton.ClickAndOpen<ClaimCreationPage>();
            createVacationPage.ClaimTypeSelect.SelectValueByText(type.GetDescription());
            if (type == ClaimType.Child) createVacationPage.ChildAgeInput.ClearAndInputText(childrenAge.ToString());
            createVacationPage.ClaimStartDatePicker.SetValue(startDate ?? GetStartDateFromNow());
            createVacationPage.ClaimEndDatePicker.SetValue(endDate ?? GetEndDateFromNow());
            createVacationPage.DirectorFioCombobox.SelectValue(directorFio);
            createVacationPage.SendButton.Click();
            page.WaitLoaded();
        }

        private DateTime GetStartDateFromNow(int daysShift = 5)
        {
            return DateTime.Now.AddDays(daysShift);
        }

        private DateTime GetEndDateFromNow(int daysShift = 12)
        {
            return DateTime.Now.AddDays(daysShift);
        }

        private string GetPeriod(DateTime startDate, DateTime endDate)
        {
            return $@"{startDate.ToString(DateFormat)} - {endDate.ToString(DateFormat)}";
        }
    }
}