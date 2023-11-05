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
        private const string Claim = "Заявление ";

        [Test]
        public void CreateChildCareVacationTest()
        {
            var employeeId = Guid.NewGuid().ToString();
            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            employeePage.ClaimList.WaitAbsence();

            var claim = new ClaimBuilder().WithUserId(employeeId).Build();
            //Claims.Claim.CreateChildType() with {UserId = employeeId};
            ClaimStorage.Add(new[] {claim});
            employeePage.Refresh();

            employeePage.ClaimList.Items.Count.Wait().EqualTo(1);

            var vacation = employeePage.ClaimList.Items.SingleOrDefault()!;
            vacation.TitleLink.Text.Wait().EqualTo(Claim + "1");
            vacation.PeriodLabel.Text.Wait().EqualTo(ConvertDate(claim.StartDate) + " - " + ConvertDate(claim.EndDate));
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
            var expect = new[] {Claim + "1", Claim + "2", Claim + "3"};

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
                (Claim + "1", ConvertDate(claimStartDate1) + " - " + ConvertDate(claimEndDate1), status),
                (Claim + "2", ConvertDate(claimStartDate2) + " - " + ConvertDate(claimEndDate2), status)
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
            var claimStartDate1 = DateTime.Today.AddDays(14);
            var claimEndDate1 = claimStartDate1.AddDays(7);
            var claimStartDate2 = DateTime.Today.AddDays(25);
            var claimEndDate2 = claimStartDate2.AddDays(7);

            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            employeePage.ClaimList.WaitAbsence();

            var claim1 = new ClaimBuilder().WithPeriod(claimStartDate1, claimEndDate1).WithUserId(employeeId).Build();
            var claim2 = new ClaimBuilder().WithPeriod(claimStartDate2, claimEndDate2).WithUserId(employeeId).Build();
            ClaimStorage.Add(new[] {claim1, claim2});
            employeePage.Refresh();

            employeePage.ClaimList.Items.Count.Wait().EqualTo(2);

            var claim = employeePage.ClaimList.Items.Wait().Single(x => x.TitleLink.Text, Is.EqualTo(Claim + "2"));
            claim.PeriodLabel.Text.Wait().EqualTo(ConvertDate(claimStartDate2) + " - " + ConvertDate(claimEndDate2));
            claim.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
        }

        private static string ConvertDate(DateTime dateTime)
        {
            return dateTime.ToString("d", new CultureInfo("ru-RU"));
        }
    }
}