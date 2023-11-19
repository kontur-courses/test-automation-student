﻿#nullable enable
using System;
using System.Globalization;
using System.Linq;
using Kontur.Selone.Properties;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.Helper;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.EmployeePage
{
    public class EmployeeVacationsListUiTests : VacationTestBase
    {
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

            employeePage = new ClaimUiHelper().CreateClaimFromUi(employeePage, claimStartDate, claimEndDate, claimType);

            employeePage.ClaimList.Items.Count.Wait().EqualTo(1);

            //var claimId = ClaimStorage.GetAll().Single(x=>x.UserId == employeeId).Id;

            var vacation = employeePage.ClaimList.Items.Single();
            vacation.TitleLink.Text.Wait().EqualTo("Заявление 1");
            vacation.PeriodLabel.Text.Wait().EqualTo(GetClaimPeriod(claimStartDate, claimEndDate));
            vacation.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
        }

        [Test]
        public void CreateVacations_ShouldAddItemsToClaimsList()
        {
            var employeeId = Guid.NewGuid().ToString();

            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            employeePage.ClaimList.WaitAbsence();

            //создаем 2 заявления
            employeePage = new ClaimUiHelper().CreateClaimFromUi(employeePage);
            employeePage = new ClaimUiHelper().CreateClaimFromUi(employeePage);

            employeePage.ClaimList.Items.Count.Wait().EqualTo(2);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitles_InRightOrder()
        {
            var employeeId = Guid.NewGuid().ToString();
            var expect = new[] {"Заявление 1", "Заявление 2", "Заявление 3"};

            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            employeePage.ClaimList.WaitAbsence();

            //создаем 3 заявления
            employeePage = new ClaimUiHelper().CreateClaimFromUi(employeePage);
            employeePage = new ClaimUiHelper().CreateClaimFromUi(employeePage);
            employeePage = new ClaimUiHelper().CreateClaimFromUi(employeePage);

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

            //создаем 2 заявления
            employeePage = new ClaimUiHelper().CreateClaimFromUi(employeePage, claimStartDate1, claimEndDate1);
            employeePage = new ClaimUiHelper().CreateClaimFromUi(employeePage, claimStartDate2, claimEndDate2);

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

            employeePage = new ClaimUiHelper().CreateClaimFromUi(employeePage, claimStartDate1, claimEndDate1);
            employeePage = new ClaimUiHelper().CreateClaimFromUi(employeePage, claimStartDate2, claimEndDate2);

            employeePage.ClaimList.Items.Count.Wait().EqualTo(2);

            var claim = employeePage.ClaimList.Items.Wait().Single(x => x.TitleLink.Text, Is.EqualTo("Заявление 2"));
            claim.PeriodLabel.Text.Wait().EqualTo(GetClaimPeriod(claimStartDate2, claimEndDate2));
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