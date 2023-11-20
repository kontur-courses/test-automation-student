#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Kontur.Selone.Pages;
using Kontur.Selone.Properties;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.Helper;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.AdminPage
{
    [NonParallelizable]
    public class AdminVacationsListTests : VacationTestBase
    {
        [Explicit("Долгий тест, создает данные через интерсфейс")]
        [Test]
        public void TestAdminClaimsList_ShouldDisplayVacationFromDifferentEmployee_CreateClaimFromUi()
        {
            var employeeId = Guid.NewGuid().ToString();
            var employee1Page = Navigation.OpenEmployeeVacationListPage();
            new ClaimUiHelper().CreateClaimFromUi(employee1Page);
            var employee2Page = Navigation.OpenEmployeeVacationListPage(employeeId);
            new ClaimUiHelper().CreateClaimFromUi(employee2Page);


            var adminPage = Navigation.OpenAdminVacationListPage();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(2);

            var expect = new[]
            {
                ("Заявление 1", "Иванов Петр Семенович"),
                ("Заявление 2", "Пользователь " + employeeId)
            };

            adminPage.ClaimList.Items.Select(x => Props.Create(x.TitleLink.Text, x.UserFioLabel.Text)).Wait()
                .EquivalentTo(expect);
        }

        [Test]
        public void TestAdminClaimsList_ShouldDisplayVacationWithAllStatus()
        {
            var expect = new[]
            {
                ("На согласовании", true, true),
                ("Согласовано", false, false),
                ("Отклонено", false, false)
            };
            var adminPage = OpenAdminPageAndCheckEmptyClaimList();

            var claim1 = Claim.CreateDefault() with {Status = ClaimStatus.NonHandled};
            var claim2 = Claim.CreateDefault() with {Status = ClaimStatus.Accepted};
            var claim3 = Claim.CreateDefault() with {Status = ClaimStatus.Rejected};

            ClaimStorage.Add(new[] {claim1, claim2, claim3});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(3);
            adminPage.ClaimList.Items
                .Select(x => Props.Create(x.StatusLabel.Text, x.AcceptButton.Present, x.RejectButton.Present)).Wait()
                .EqualTo(expect);
        }

        [TestCase(ClaimStatus.Accepted, "27")]
        [TestCase(ClaimStatus.Rejected, "28")]
        [TestCase(ClaimStatus.NonHandled, "27")]
        public void TestAdminClaimsList_CheckClaimInfo_OpenClaimAndCheckInfo(ClaimStatus status,
            string availableDaysCount)
        {
            var adminPage = OpenAdminPageAndCheckEmptyClaimList();

            var claim = new ClaimBuilder().WithStatus(status).Build();
            ClaimStorage.Add(new[] {claim});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(1);
            var claimRow = adminPage.ClaimList.Items.Single();
            claimRow.TitleLink.Text.Wait().EqualTo($"Заявление {claim.Id}");
            claimRow.UserFioLabel.Text.Wait().EqualTo("Иванов Петр Семенович");
            claimRow.PeriodLabel.Text.Wait().EqualTo(GetClaimPeriodFromList(claim.StartDate, claim.EndDate));
            claimRow.StatusLabel.Text.Wait().EqualTo(status.GetDescription());
            if (status == ClaimStatus.NonHandled)
            {
                claimRow.AcceptButton.WaitEnabled();
                claimRow.RejectButton.WaitEnabled();
            }
            else
            {
                claimRow.AcceptButton.WaitAbsence();
                claimRow.RejectButton.WaitAbsence();
            }

            var claimModal = adminPage.ClaimList.Items.Single().TitleLink.ClickAndOpen<ClaimLightbox>();
            CheckClaimInfoInModal(claimModal, claim, availableDaysCount);
        }

        [Test]
        public void TestAdminClaimsList_CheckOrderTest()
        {
            var employee2Id = Guid.NewGuid().ToString();
            var employee3Id = Guid.NewGuid().ToString();
            var adminPage = OpenAdminPageAndCheckEmptyClaimList();

            var claim1 = new ClaimBuilder().WithUserId(1).Build();
            var claim2 = new ClaimBuilder().WithUserId(employee2Id).Build();
            var claim3 = new ClaimBuilder().WithUserId(employee3Id).Build();
            ClaimStorage.Add(new[] {claim3, claim1, claim2});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Select(x => x.TitleLink.Text).Wait().EqualTo(new[]
                {$"Заявление {claim3.Id}", $"Заявление {claim1.Id}", $"Заявление {claim2.Id}"});
        }

        [Test]
        public void TestAdminClaimsList_ListAcceptButtonTest()
        {
            var adminPage = OpenAdminPageAndCheckEmptyClaimList();

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
        public void TestAdminClaimsList_ListRejectButtonTest()
        {
            var adminPage = OpenAdminPageAndCheckEmptyClaimList();

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

        [TestCase(ClaimStatus.Accepted)]
        [TestCase(ClaimStatus.Rejected)]
        [TestCase(ClaimStatus.NonHandled)]
        public void TestAdminClaimsList_ModalAcceptButtonTest(ClaimStatus status)
        {
            var finalStatus = ClaimStatus.Accepted;
            var adminPage = OpenAdminPageAndCheckEmptyClaimList();

            var claim = new ClaimBuilder().WithStatus(status).Build();
            ClaimStorage.Add(new[] {claim});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(1);
            var claimModal = adminPage.ClaimList.Items.Single().TitleLink.ClickAndOpen<ClaimLightbox>();
            adminPage = claimModal.Footer.AcceptButton.ClickAndOpen<AdminVacationListPage>();
            adminPage.ClaimList.Items.Single().StatusLabel.Text.Wait().EqualTo(finalStatus.GetDescription());
            claimModal = adminPage.ClaimList.Items.Single().TitleLink.ClickAndOpen<ClaimLightbox>();
            claimModal.StatusLabel.Text.Wait().EqualTo(finalStatus.GetDescription());

            var claimStatus = ClaimStorage.GetAll().Single(x => x.Id == claim.Id).Status;
            Assert.AreEqual(finalStatus, claimStatus);
        }

        [TestCase(ClaimStatus.Accepted)]
        [TestCase(ClaimStatus.Rejected)]
        [TestCase(ClaimStatus.NonHandled)]
        public void TestAdminClaimsList_ModalRejectButtonTest(ClaimStatus status)
        {
            var finalStatus = ClaimStatus.Rejected;
            var adminPage = OpenAdminPageAndCheckEmptyClaimList();

            var claim = new ClaimBuilder().WithStatus(status).Build();
            ClaimStorage.Add(new[] {claim});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(1);
            var claimModal = adminPage.ClaimList.Items.Single().TitleLink.ClickAndOpen<ClaimLightbox>();
            adminPage = claimModal.Footer.RejectButton.ClickAndOpen<AdminVacationListPage>();
            adminPage.ClaimList.Items.Single().StatusLabel.Text.Wait().EqualTo(finalStatus.GetDescription());
            claimModal = adminPage.ClaimList.Items.Single().TitleLink.ClickAndOpen<ClaimLightbox>();
            claimModal.StatusLabel.Text.Wait().EqualTo(finalStatus.GetDescription());

            var claimStatus = ClaimStorage.GetAll().Single(x => x.Id == claim.Id).Status;
            Assert.AreEqual(finalStatus, claimStatus);
        }

        //дублирование тестов, используем TestCaseSource
        private static IEnumerable<TestCaseData> CasesForListButtonTest()
        {
            yield return new TestCaseData(new Func<AdminClaimItem, Button>(item => item.AcceptButton),
                ClaimStatus.Accepted).SetName("AcceptedTest"); // 1й тест-кейс
            yield return new TestCaseData(new Func<AdminClaimItem, Button>(item => item.RejectButton),
                ClaimStatus.Rejected).SetName("RejectTest");
        }

        [TestCaseSource(nameof(CasesForListButtonTest))]
        public void TestAdminClaimsList_ListButtonTest(Func<AdminClaimItem, Button> getButton, ClaimStatus status)
        {
            var adminPage = OpenAdminPageAndCheckEmptyClaimList();

            var claim = new ClaimBuilder().Build();
            ClaimStorage.Add(new[] {claim});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(1);
            var vacation = adminPage.ClaimList.Items.Single();
            vacation.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
            vacation.AcceptButton.WaitEnabled();
            vacation.RejectButton.WaitEnabled();

            getButton(vacation).Click();

            vacation.StatusLabel.Text.Wait().EqualTo(status.GetDescription());
            var claimStatus = ClaimStorage.GetAll().Single(x => x.Id == claim.Id).Status;
            Assert.AreEqual(status, claimStatus);
            vacation.AcceptButton.WaitAbsence();
            vacation.RejectButton.WaitAbsence();
        }

        private static IEnumerable<TestCaseData> CasesForAcceptButtonTest()
        {
            yield return new TestCaseData(new Func<AdminClaimItem, Button>(item => item.AcceptButton)).SetName(
                "AcceptedTestFromList");
            //Верно понимаю, что входные и выходные типы данных должны быть одинаковые для группы тест-кейсов?
            yield return new TestCaseData(new Func<AdminClaimItem, Button>(item =>
                item.TitleLink.ClickAndOpen<ClaimLightbox>().Footer.AcceptButton)).SetName("AcceptedTestFromModal");
        }

        [TestCaseSource(nameof(CasesForAcceptButtonTest))]
        public void TestAdminClaimsList_AcceptButtonTest(Func<AdminClaimItem, Button> getButton)
        {
            var adminPage = OpenAdminPageAndCheckEmptyClaimList();

            var claim = new ClaimBuilder().Build();
            ClaimStorage.Add(new[] {claim});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(1);
            var vacation = adminPage.ClaimList.Items.Single();
            vacation.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
            vacation.AcceptButton.WaitEnabled();
            vacation.RejectButton.WaitEnabled();

            getButton.Invoke(vacation).Click();

            vacation.StatusLabel.Text.Wait().EqualTo(ClaimStatus.Accepted.GetDescription());
            var claimStatus = ClaimStorage.GetAll().Single(x => x.Id == claim.Id).Status;
            Assert.AreEqual(ClaimStatus.Accepted, claimStatus);
            vacation.AcceptButton.WaitAbsence();
            vacation.RejectButton.WaitAbsence();
        }

        //дальше моё творчество

        [Test]
        public void TestAdminClaimsList_ShouldDisplayVacationFromDifferentEmployee()
        {
            var employeeId = Guid.NewGuid().ToString();
            var adminPage = OpenAdminPageAndCheckEmptyClaimList();

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
        public void TestAdminClaimsList_OpenClaimAndCheckInfo(ClaimStatus status)
        {
            var adminPage = OpenAdminPageAndCheckEmptyClaimList();

            var claim = new ClaimBuilder().WithStatus(status).Build();
            ClaimStorage.Add(new[] {claim});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(1);
            var claimModal = adminPage.ClaimList.Items.Single().TitleLink.ClickAndOpen<ClaimLightbox>();
            CheckClaimInfoInModal(claimModal, claim);
        }

        [Test]
        public void TestAdminClaimsList_ShouldDisplayVacation_WithAllStatus_AndWhenVacationDateInPast()
        {
            var startDate = DateTime.Today.AddDays(-14);
            var endDate = startDate.AddDays(3);
            var adminPage = OpenAdminPageAndCheckEmptyClaimList();

            var claim1 = new ClaimBuilder().WithPeriod(startDate, endDate).WithStatus(ClaimStatus.NonHandled).Build();
            var claim2 = new ClaimBuilder().WithPeriod(startDate, endDate).WithStatus(ClaimStatus.Accepted).Build();
            var claim3 = new ClaimBuilder().WithPeriod(startDate, endDate).WithStatus(ClaimStatus.Rejected).Build();
            ClaimStorage.Add(new[] {claim1, claim2, claim3});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(3);
            adminPage.ClaimList.Items.Select(x => x.StatusLabel.Text).Wait()
                .EqualTo(new[] {"На согласовании", "Согласовано", "Отклонено"});
        }


        [Test]
        public void TestAdminClaimsList_DownloadButtonTest()
        {
            var adminPage = OpenAdminPageAndCheckEmptyClaimList();

            adminPage.DownloadButton.WaitAbsence();

            var claim1 = new ClaimBuilder().WithUserId(1).Build();
            ClaimStorage.Add(new[] {claim1});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(1);
            adminPage.DownloadButton.WaitDisabled();

            var claim2 = new ClaimBuilder().WithUserId(2).Build();
            var claim3 = new ClaimBuilder().WithUserId(2).Build();
            ClaimStorage.Add(new[] {claim2, claim3});
            adminPage.Refresh();

            adminPage.ClaimList.Items.Count.Wait().EqualTo(3);

            adminPage.ClaimList.Items.ElementAtOrDefault(0)!.ItemCheckbox.SetChecked();
            adminPage.DownloadButton.WaitEnabled();
            adminPage.ClaimList.Items.ElementAtOrDefault(0)!.ItemCheckbox.SetUnchecked();
            adminPage.DownloadButton.WaitDisabled();
            //todo e.lopatina 08/11/23 дописать проверку скаченного файла с эталонным
            //выделить отпуск 1 и 3, проверить что кнопка активна
            //жмякнуть кнопку, проверить что скачалось нужное
        }

        private AdminVacationListPage OpenAdminPageAndCheckEmptyClaimList()
        {
            var adminPage = Navigation.OpenAdminVacationListPage();
            ClaimStorage.ClearClaims();
            adminPage.Refresh();
            adminPage.ClaimList.WaitAbsence();
            return adminPage;
        }

        private static void CheckClaimInfoInModal(ClaimLightbox claimModal, Claim claim,
            string? availableDaysCount = null)
        {
            //todo (e.lopatina 08.11.23) Не понятно откуда берется ФИО для юзера 1, надо бы узнать и переписать проверку
            claimModal.ModalHeaderLabel.Text.Wait().EqualTo("Иванов Петр Семенович");
            claimModal.StatusLabel.Text.Wait().EqualTo(claim.Status.GetDescription());
            claimModal.ClaimTypeLabel.Text.Wait().EqualTo(claim.Type.GetDescription());
            claimModal.PeriodLabel.Text.Wait().EqualTo(GetClaimPeriodFromModal(claim.StartDate, claim.EndDate));
            if (availableDaysCount != null)
                claimModal.AvailableDaysLabel.Text.Wait().EqualTo(availableDaysCount);
            claimModal.DirectorFioLabel.Text.Wait().EqualTo(claim.Director.Name);
            if (claim.Status == ClaimStatus.NonHandled)
                claimModal.PayNowCheckbox.WaitDisabled();
            else
                claimModal.PayNowCheckbox.WaitEnabled();
            claimModal.PayNowCheckbox.Checked.Wait().EqualTo(true);
            claimModal.Footer.AcceptButton.WaitEnabled();
            claimModal.Footer.RejectButton.WaitEnabled();
            claimModal.CrossButton.ClickAndOpen<AdminVacationListPage>();
            //todo (e.lopatin 08.11.23): хотела проверить, что модалка закрылась, но что-то не понимаю, как это сделать
            claimModal.ClaimModal.WaitAbsence();
        }

        private static string GetClaimPeriodFromList(DateTime claimStartDate, DateTime claimEndDate)
        {
            return ConvertDate(claimStartDate) + " - " + ConvertDate(claimEndDate);
        }

        private static string GetClaimPeriodFromModal(DateTime claimStartDate, DateTime claimEndDate)
        {
            return ConvertDate(claimStartDate) + " — " + ConvertDate(claimEndDate);
        }

        private static string ConvertDate(DateTime dateTime)
        {
            return dateTime.ToString("d", new CultureInfo("ru-RU"));
        }
    }
}