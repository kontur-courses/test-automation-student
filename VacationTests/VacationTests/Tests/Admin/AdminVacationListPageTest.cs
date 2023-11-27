using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.RetryableAssertions;
using Kontur.Selone.Pages;
using Kontur.Selone.Properties;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;
using VacationTests.Utils;

namespace VacationTests.Tests.Admin
{
    public class AdminVacationListPageTest : VacationTestBase
    {

        [TearDown]
        public void SetUp()
        {
            ClaimStorage.ClearClaims();
        }

        [Test]
        public void AdminClaimsList_ShouldDisplayRightData_InRightOrder()
        {
            var firstClaim = Claim.CreateDefault() with
            {
                Id = "1",
                StartDate = DateTimeUtils.GetStartDateFromNow(),
                EndDate = DateTimeUtils.GetEndDateFromNow()
            };

            var secondClaim = Claim.CreateDefault() with
            {
                Id = "2",
                StartDate = DateTimeUtils.GetStartDateFromNow(),
                EndDate = DateTimeUtils.GetEndDateFromNow()
            };

            var pageVacations = Navigation.OpenEmployeeVacationListPage("2");
            pageVacations.WaitLoaded();

            var claimCreationPage = pageVacations.CreateButton.ClickAndOpen<ClaimCreationPage>();
            claimCreationPage.CreateClaimFromUI(firstClaim);
            pageVacations = claimCreationPage.SendButton.ClickAndOpen<EmployeeVacationListPage>();
            pageVacations.WaitLoaded();


            pageVacations = Navigation.OpenEmployeeVacationListPage("3");
            pageVacations.WaitLoaded();
            claimCreationPage = pageVacations.CreateButton.ClickAndOpen<ClaimCreationPage>();

            claimCreationPage.CreateClaimFromUI(secondClaim);
            pageVacations = claimCreationPage.SendButton.ClickAndOpen<EmployeeVacationListPage>();
            pageVacations.WaitLoaded();


            var expectedClaimsList = new[]
            {
                (
                    "Заявление 1",
                    "Пользователь 2",
                    DateTimeUtils.GetPeriod(firstClaim.StartDate, firstClaim.EndDate),
                    ClaimStatus.NonHandled.GetDescription()
                ),
                (
                    "Заявление 2",
                    "Пользователь 3",
                    DateTimeUtils.GetPeriod(secondClaim.StartDate, secondClaim.EndDate),
                    ClaimStatus.NonHandled.GetDescription()
                )
            };

            var adminVacationListPage = Navigation.OpenAdminVacationListPage();
            adminVacationListPage.ClaimList.Items.Select(
                p => Props.Create(
                    p.TitleLink.Text,
                    p.UserFioLabel.Text,
                    p.PeriodLabel.Text,
                    p.StatusLabel.Text
                )).Wait().EqualTo(expectedClaimsList);
        }


        [Test]
        public void AdminClaimsList_ShouldDisplayRightStatusAndApproveControl_ForItem()
        {
            var adminVacationListPage = Navigation.OpenAdminVacationListPage();
            ClaimStorage.Add(new[]
            {
                Claim.CreateDefault() with { Status = ClaimStatus.Accepted },
                Claim.CreateDefault() with { Status = ClaimStatus.Rejected },
                Claim.CreateDefault() with { Status = ClaimStatus.NonHandled }
            });
            adminVacationListPage.Refresh();

            var expectedVacationList = new[]
            {
                (
                    ClaimStatus.Accepted.GetDescription(),
                    false,
                    false
                ),
                (
                    ClaimStatus.Rejected.GetDescription(),
                    false,
                    false
                ),
                (
                    ClaimStatus.NonHandled.GetDescription(),
                    true,
                    true
                )
            };

            adminVacationListPage.ClaimList.Items.Select(
                element => Props.Create(
                    element.StatusLabel.Text,
                    element.AcceptButton.Present,
                    element.RejectButton.Present
                )
            ).Wait().EquivalentTo(expectedVacationList);
        }

        [TestCaseSource(nameof(ClaimsDataCases))]
        public void AdminClaimsList_ShouldDisplayRightData(Claim expectedClaim)
        {
            var adminVacationListPage = Navigation.OpenAdminVacationListPage();
            adminVacationListPage.ClaimList.Items.Count.Wait().EqualTo(0);

            ClaimStorage.Add(expectedClaim);
            adminVacationListPage.Refresh();

            var actualClaim = adminVacationListPage.ClaimList.Items.Wait().Single();

            actualClaim.TitleLink.Text.Wait().EqualTo(expectedClaim.GetTitle());
            actualClaim.UserFioLabel.Text.Wait().EqualTo(expectedClaim.GetUserFIO());
            actualClaim.PeriodLabel.Text.Wait()
                .EqualTo(DateTimeUtils.GetPeriod(expectedClaim.StartDate, expectedClaim.EndDate));
            actualClaim.StatusLabel.Text.Wait().EqualTo(expectedClaim.Status.GetDescription());

            var claimLightBox = actualClaim.TitleLink.ClickAndOpen<ClaimLightbox>();

            claimLightBox.ModalHeaderLabel.Text.Wait().EqualTo(expectedClaim.GetUserFIO());
            claimLightBox.StatusLabel.Text.Wait().EqualTo(expectedClaim.Status.GetDescription());
            claimLightBox.ClaimTypeLabel.Text.Wait().EqualTo(expectedClaim.Type.GetDescription());
            if (expectedClaim.Type == ClaimType.Child)
                claimLightBox.ChildAgeLabel.Text.Wait().EqualTo(expectedClaim.ChildAgeInMonths.ToString());

            claimLightBox.PeriodLabel.Text.Wait()
                .EqualTo(DateTimeUtils.GetPeriod(expectedClaim.StartDate, expectedClaim.EndDate, "—"));
            claimLightBox.DirectorFioLabel.Text.Wait().EqualTo(expectedClaim.Director.Name);
            claimLightBox.PayNowCheckbox.Checked.Wait().EqualTo(expectedClaim.PaidNow);
        }
        
        private static IEnumerable<TestCaseData> ClaimsDataCases()
        {
            yield return new TestCaseData(
                Claim.CreateDefault() with
                {
                    UserId = "2",
                    PaidNow = true
                }
                ).SetName("Оплачеваемый отпуск");
            yield return new TestCaseData(
                Claim.CreateChildType() with
                {
                    UserId = "3",
                    Type = ClaimType.Child,
                    PaidNow = true
                }
                ).SetName("Отпуск по уходу за ребенком");
        }

        [TestCaseSource(nameof(ApproveCases))]
        public void AdminClaimsList_ApproveClaim_ShouldDisplayRightStatus(Action<AdminVacationListPage> action, ClaimStatus expectedStatus)
        {
            var adminVacationListPage = Navigation.OpenAdminVacationListPage();
            ClaimStorage.Add(Claim.CreateDefault());
            adminVacationListPage.Refresh();
            action(adminVacationListPage);
            
            adminVacationListPage.ClaimList.Items.Wait().Single()
                .StatusLabel
                .Text.Wait()
                .EqualTo(expectedStatus.GetDescription());
            
            adminVacationListPage.ClaimList.Items.Wait().Single()
                .TitleLink
                .ClickAndOpen<ClaimLightbox>()
                .StatusLabel
                .Text.Wait()
                .EqualTo(expectedStatus.GetDescription());
        }
        
        private static IEnumerable<TestCaseData> ApproveCases()
        {
            yield return new TestCaseData(
                new Action<AdminVacationListPage>(page => page.ClaimList.Items.Wait().Single().AcceptButton.Click()), 
                ClaimStatus.Accepted
            ).SetName("Подтверждение из списка заявлений.");
            
            yield return new TestCaseData( 
                new Action<AdminVacationListPage>(page =>
                {
                    var claimLightbox = page.ClaimList.Items.Wait().Single().TitleLink.ClickAndOpen<ClaimLightbox>();
                    claimLightbox.AcceptButton.Click();
                }),
                ClaimStatus.Accepted
            ).SetName("Подтверждение из лайтбокса заявления.");
            
            yield return new TestCaseData(
                new Action<AdminVacationListPage>(page => page.ClaimList.Items.Wait().Single().RejectButton.Click()),
                ClaimStatus.Rejected
            ).SetName("Отклониие из списка заявлений.");
            
            yield return new TestCaseData(
                new Action<AdminVacationListPage>(page =>
                {
                    var claimLightbox = page.ClaimList.Items.Wait().Single().TitleLink.ClickAndOpen<ClaimLightbox>();
                    claimLightbox.RejectButton.Click();
                }),
                ClaimStatus.Rejected
            ).SetName("Отклоение из лайтбокса заявления.");
        }

        [Test]
        public void AdminClaimsList_CreateTwoClaims_ShouldDisplayInOrder()
        {
            var adminVacationListPage = Navigation.OpenAdminVacationListPage();
            ClaimStorage.Add(new[]
            {
                Claim.CreateDefault() with { Id = "1"},
                Claim.CreateDefault() with { Id = "2" }
            });
            adminVacationListPage.Refresh();
            
            var expectedVacationList = new[]
            {
                "Заявление 1",
                "Заявление 2"
            };

            adminVacationListPage.ClaimList.Items.Select(
                element => element.TitleLink.Text
                ).Wait().EqualTo(expectedVacationList);
        }
    }
}