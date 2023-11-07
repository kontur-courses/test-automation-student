using System;
using System.Globalization;
using System.Linq;
using Kontur.Selone.Pages;
using Kontur.Selone.Properties;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.Tests.EmployeePage
{
    public class EmployeeVacationsListTests : VacationTestBase
    {
        [Test]
        public void CreateChildCareVacationTest()
        {
            var employeeId = Guid.NewGuid().ToString();
            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            employeePage.ClaimList.WaitAbsence();

            var claim = new ClaimBuilder().WithUserId(employeeId).Build();
            ClaimStorage.Add(new[] {claim});
            employeePage.Refresh();

            employeePage.ClaimList.Items.Count.Wait().EqualTo(1);

            var vacation = employeePage.ClaimList.Items.SingleOrDefault()!;
            vacation.TitleLink.Text.Wait().EqualTo("Заявление 1");
            vacation.PeriodLabel.Text.Wait().EqualTo(GetClaimPeriod(claim.StartDate, claim.EndDate));
            vacation.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
        }

        [Test]
        public void CreateVacations_ShouldAddItemsToClaimsList()
        {
            var employeeId = Guid.NewGuid().ToString();
            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            employeePage.ClaimList.WaitAbsence();

            var claim1 = new ClaimBuilder().WithUserId(employeeId).Build();
            var claim2 = new ClaimBuilder().WithUserId(employeeId).Build();
            ClaimStorage.Add(new[] {claim1, claim2});
            employeePage.Refresh();

            employeePage.ClaimList.Items.Count.Wait().EqualTo(2);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitles_InRightOrder()
        {
            var employeeId = Guid.NewGuid().ToString();
            var expect = new[] {"Заявление 1", "Заявление 2", "Заявление 3"};

            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            employeePage.ClaimList.WaitAbsence();

            var claim1 = new ClaimBuilder().WithUserId(employeeId).Build();
            var claim2 = new ClaimBuilder().WithUserId(employeeId).Build();
            var claim3 = new ClaimBuilder().WithUserId(employeeId).Build();
            ClaimStorage.Add(new[] {claim1, claim2, claim3});
            employeePage.Refresh();

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
                ("Заявление 1", GetClaimPeriod(claimStartDate1, claimEndDate1), status),
                ("Заявление 2", GetClaimPeriod(claimStartDate2, claimEndDate2), status)
            };

            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            employeePage.ClaimList.WaitAbsence();

            var claim1 = new ClaimBuilder().WithPeriod(claimStartDate1, claimEndDate1).WithUserId(employeeId).Build();
            var claim2 = new ClaimBuilder().WithPeriod(claimStartDate2, claimEndDate2).WithUserId(employeeId).Build();
            ClaimStorage.Add(new[] {claim1, claim2});
            employeePage.Refresh();

            employeePage.ClaimList.Items.Count.Wait().EqualTo(2);
            employeePage.ClaimList.Items
                .Select(x => Props.Create(x.TitleLink.Text, x.PeriodLabel.Text, x.StatusLabel.Text)).Wait()
                .EquivalentTo(expect);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightPeriodForItem()
        {
            var employeeId = Guid.NewGuid().ToString();
            var claim2StartDate = DateTime.Today.AddDays(25);
            var claim2EndDate = claim2StartDate.AddDays(7);

            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            employeePage.ClaimList.WaitAbsence();

            var claim1 = new ClaimBuilder().WithUserId(employeeId).Build();
            var claim2 = new ClaimBuilder().WithPeriod(claim2StartDate, claim2EndDate).WithUserId(employeeId).Build();
            ClaimStorage.Add(new[] {claim1, claim2});
            employeePage.Refresh();

            employeePage.ClaimList.Items.Count.Wait().EqualTo(2);

            var claim = employeePage.ClaimList.Items.Wait().Single(x => x.TitleLink.Text, Is.EqualTo("Заявление 2"));
            claim.PeriodLabel.Text.Wait().EqualTo(GetClaimPeriod(claim2StartDate, claim2EndDate));
            claim.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
        }

        private static string GetClaimPeriod(DateTime claimStartDate, DateTime claimEndDate)
        {
            return ConvertDate(claimStartDate) + " - " + ConvertDate(claimEndDate);
        }

        private static string ConvertDate(DateTime dateTime)
        {
            return dateTime.ToString("d", new CultureInfo("ru-RU"));
        }
    }
}