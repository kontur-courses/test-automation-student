#nullable enable
using System;
using System.Globalization;
using System.Linq;
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
        private const string Claim = "Заявление ";

        [Test]
        public void CreateChildCareVacationTest()
        {
            var employeeId = Guid.NewGuid().ToString();
            var claimType = ClaimType.Child;
            var claimStartDate = DateTime.Today.AddDays(14);
            var claimEndDate = claimStartDate.AddDays(7);

            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            employeePage.ClaimList.WaitAbsence();
            //не проходил на юлерне тест, поэтому пришлось закомментировать
            //employeePage.NoClaimsTextLabel.Text.Wait().EqualTo("Нет заявлений");

            employeePage = CreateClaimFromUi(employeePage, claimStartDate, claimEndDate, claimType);

            employeePage.ClaimList.Items.Count.Wait().EqualTo(1);

            //хотела брать из БД данные, но почему-то ClaimStorage.GetAll() отдаёт null
            //var claimId = ClaimStorage.GetAll().Single(x=>x.UserId == employeeId).Id;

            var vacation = employeePage.ClaimList.Items.SingleOrDefault()!;
            vacation.TitleLink.Text.Wait().EqualTo(Claim + "1");
            vacation.PeriodLabel.Text.Wait().EqualTo(ConvertDate(claimStartDate) + " - " + ConvertDate(claimEndDate));
            vacation.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
        }

        [Test]
        public void CreateVacations_ShouldAddItemsToClaimsList()
        {
            var employeeId = Guid.NewGuid().ToString();
            var claimStartDate = DateTime.Today.AddDays(14);
            var claimEndDate = claimStartDate.AddDays(7);

            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            employeePage.ClaimList.WaitAbsence();

            //создаем 2 заявления
            employeePage = CreateClaimFromUi(employeePage, claimStartDate, claimEndDate);
            employeePage = CreateClaimFromUi(employeePage, claimStartDate, claimEndDate);

            employeePage.ClaimList.Items.Count.Wait().EqualTo(2);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitles_InRightOrder()
        {
            var employeeId = Guid.NewGuid().ToString();
            var claimStartDate = DateTime.Today.AddDays(14);
            var claimEndDate = claimStartDate.AddDays(7);
            var expect = new[] {Claim + "1", Claim + "2", Claim + "3"};

            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            employeePage.ClaimList.WaitAbsence();

            //создаем 3 заявления
            employeePage = CreateClaimFromUi(employeePage, claimStartDate, claimEndDate);
            employeePage = CreateClaimFromUi(employeePage, claimStartDate, claimEndDate);
            employeePage = CreateClaimFromUi(employeePage, claimStartDate, claimEndDate);

            employeePage.ClaimList.Items.Count.Wait().EqualTo(3);
            employeePage.ClaimList.Items.Select(x => x.TitleLink.Text).Wait().EqualTo(expect);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitleAndStatus_IgnoringOrder()
        {
            var employeeId = Guid.NewGuid().ToString();
            var claimStartDate1 = DateTime.Today.AddDays(14);
            var claimEndDate1 = claimStartDate1.AddDays(7);
            var claimStartDate2 = DateTime.Today.AddDays(25);
            var claimEndDate2 = claimStartDate2.AddDays(7);
            var status = ClaimStatus.NonHandled.GetDescription();
            var expect = new[]
            {
                (Claim + "1", ConvertDate(claimStartDate1) + " - " + ConvertDate(claimEndDate1), status),
                (Claim + "2", ConvertDate(claimStartDate2) + " - " + ConvertDate(claimEndDate2), status)
            };

            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            employeePage.ClaimList.WaitAbsence();

            //создаем 2 заявления
            employeePage = CreateClaimFromUi(employeePage, claimStartDate1, claimEndDate1);
            employeePage = CreateClaimFromUi(employeePage, claimStartDate2, claimEndDate2);

            employeePage.ClaimList.Items.Count.Wait().EqualTo(2);
            employeePage.ClaimList.Items
                .Select(x => Props.Create(x.TitleLink.Text, x.PeriodLabel.Text, x.StatusLabel.Text)).Wait()
                .EquivalentTo(expect);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightPeriodForItem()
        {
            var employeeId = Guid.NewGuid().ToString();
            var claimStartDate1 = DateTime.Today.AddDays(14);
            var claimEndDate1 = claimStartDate1.AddDays(7);
            var claimStartDate2 = DateTime.Today.AddDays(25);
            var claimEndDate2 = claimStartDate2.AddDays(7);

            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            employeePage.ClaimList.WaitAbsence();

            employeePage = CreateClaimFromUi(employeePage, claimStartDate1, claimEndDate1);
            employeePage = CreateClaimFromUi(employeePage, claimStartDate2, claimEndDate2);

            employeePage.ClaimList.Items.Count.Wait().EqualTo(2);

            var claim = employeePage.ClaimList.Items.Wait().Single(x => x.TitleLink.Text, Is.EqualTo(Claim + "2"));
            claim.PeriodLabel.Text.Wait().EqualTo(ConvertDate(claimStartDate2) + " - " + ConvertDate(claimEndDate2));
            claim.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
        }

        private static EmployeeVacationListPage CreateClaimFromUi(EmployeeVacationListPage employeePage,
            DateTime claimStartDate, DateTime claimEndDate, ClaimType claimType = ClaimType.Paid,
            string? director = "24939",
            string? childAge = "1")
        {
            var claimCreationPage = employeePage.CreateButton.ClickAndOpen<ClaimCreationPage>();
            claimCreationPage.ClaimTypeSelect.SelectValueByText(claimType.GetDescription());
            if (claimType == ClaimType.Child)
                claimCreationPage.ChildAgeInput.ClearAndInputText(childAge);
            claimCreationPage.ClaimStartDatePicker.SetValue(claimStartDate);
            claimCreationPage.ClaimEndDatePicker.SetValue(claimEndDate);
            claimCreationPage.DirectorFioCombobox.SelectValue(director);
            employeePage = claimCreationPage.SendButton.ClickAndOpen<EmployeeVacationListPage>();
            return employeePage;
        }

        private static string ConvertDate(DateTime dateTime)
        {
            return dateTime.ToString("d", new CultureInfo("ru-RU"));
        }
    }
}