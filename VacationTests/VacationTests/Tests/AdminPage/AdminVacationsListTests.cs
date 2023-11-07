using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Kontur.Selone.Pages;
using Kontur.Selone.Properties;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.Tests.AdminPage
{
    public class AdminVacationsListTests : VacationTestBase
    {
        [Test]
        public void TestAdminClaimsList_ShouldDisplayVacationFromDifferentEmployee()
        {
            var employeeId = Guid.NewGuid().ToString();
            var adminPage = Navigation.OpenAdminVacationListPage();

            adminPage.ClaimList.WaitAbsence();

            var claim1 = new ClaimBuilder().WithUserId(1).Build();
            var claim2 = new ClaimBuilder().WithUserId(employeeId).Build();
            ClaimStorage.Add(new[] {claim1, claim2});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(2);

            var expect = new[]
            {
                ("Заявление " + claim1.Id, "Иванов Петр Семенович"),
                ("Заявление " + claim2.Id, "Пользователь " + employeeId)
            };

            adminPage.ClaimList.Items.Select(x => Props.Create(x.TitleLink.Text, x.UserFioLabel.Text)).Wait()
                .EquivalentTo(expect);
        }

        [TestCase(ClaimStatus.Accepted)]
        [TestCase(ClaimStatus.Rejected)]
        [TestCase(ClaimStatus.NonHandled)]
        public void TestAdminClaimsList_ShouldDisplayOldVacation_WhenVacationDateInPast(ClaimStatus status)
        {
            var employeeId = Guid.NewGuid().ToString();
            var startDate = DateTime.Today.AddDays(-14);
            var endDate = startDate.AddDays(1);
            var adminPage = Navigation.OpenAdminVacationListPage();

            adminPage.ClaimList.WaitAbsence();

            var claim = new ClaimBuilder().WithPeriod(startDate, endDate).WithStatus(status).Build();
            ClaimStorage.Add(new[] {claim});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(1);
            adminPage.ClaimList.Items.Single().StatusLabel.Text.Wait().EqualTo(status.GetDescription());
        }

        [Test]
        public void TestAdminClaimsList_AcceptButtonTest()
        {
            var adminPage = Navigation.OpenAdminVacationListPage();

            adminPage.ClaimList.WaitAbsence();

            var claim = new ClaimBuilder().Build();
            ClaimStorage.Add(new[] {claim});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(1);
            var vacation = adminPage.ClaimList.Items.Single();

            vacation.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
            vacation.AcceptButton.WaitEnabled();
            vacation.RejectButton.WaitEnabled();

            vacation.AcceptButton.Click();

            vacation.StatusLabel.Text.Wait().EqualTo(ClaimStatus.Accepted.GetDescription());
            var claimStatus = ClaimStorage.GetAll().Single(x => x.Id == claim.Id).Status;
            Assert.AreEqual(ClaimStatus.Accepted, claimStatus);
            vacation.AcceptButton.WaitAbsence();
            vacation.RejectButton.WaitAbsence();
        }

        [Test]
        public void TestAdminClaimsList_RejectButtonTest()
        {
            var adminPage = Navigation.OpenAdminVacationListPage();

            adminPage.ClaimList.WaitAbsence();

            var claim = new ClaimBuilder().Build();
            ClaimStorage.Add(new[] {claim});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(1);
            var vacation = adminPage.ClaimList.Items.Single();

            vacation.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
            vacation.AcceptButton.WaitEnabled();
            vacation.RejectButton.WaitEnabled();

            vacation.RejectButton.Click();

            vacation.StatusLabel.Text.Wait().EqualTo(ClaimStatus.Rejected.GetDescription());
            var claimStatus = ClaimStorage.GetAll().Single(x => x.Id == claim.Id).Status;
            Assert.AreEqual(ClaimStatus.Rejected, claimStatus);
            vacation.AcceptButton.WaitAbsence();
            vacation.RejectButton.WaitAbsence();
        }

        [Test]
        public void TestAdminClaimsList_DownloadButtonTest()
        {
            var adminPage = Navigation.OpenAdminVacationListPage();

            adminPage.ClaimList.WaitAbsence();
            adminPage.DownloadButton.WaitAbsence();

            var claim1 = new ClaimBuilder().WithUserId(1).Build();
            ClaimStorage.Add(new[] {claim1});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(1);
            adminPage.DownloadButton.WaitDisabled();

            var claim2 = new ClaimBuilder().WithUserId(2).Build();
            var claim3 = new ClaimBuilder().WithUserId(3).Build();
            ClaimStorage.Add(new[] {claim2, claim3});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(3);

            adminPage.ClaimList.Items.ElementAtOrDefault(0)!.ItemCheckbox.SetChecked();
            adminPage.DownloadButton.WaitEnabled();
            adminPage.ClaimList.Items.ElementAtOrDefault(0)!.ItemCheckbox.SetUnchecked();
            adminPage.DownloadButton.WaitDisabled();
            //todo e.lopatina 08/11/23 дописать проверку скаченного файла с эталонным
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