using System;
using System.Linq;
using Kontur.Selone.Pages;
using Kontur.Selone.Properties;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;

namespace VacationTests.Tests.EmployeePage
{
    public class EmployeeVacationsListTests : VacationTestBase
    {
        private const string DateFormat = "dd.MM.yyyy";

        [Test]
        public void CreateVacations_ShouldAddItemsToClaimsList()
        {
            var vacationPage = Navigation.OpenEmployeeVacationListPage();
            vacationPage.WaitLoaded();
            vacationPage.ClaimList.Items.Count.Wait().EqualTo(0);
            ClaimStorage.Add(new []
            {
                Claim.CreateDefault(),
                Claim.CreateDefault()
            });
            vacationPage.Refresh();
            
            vacationPage.ClaimList.Items.Count.Wait().EqualTo(2);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitles_InRightOrder()
        {
            const string userId = "2";
            var vacationPage = Navigation.OpenEmployeeVacationListPage(userId);
            vacationPage.WaitLoaded();
            vacationPage.ClaimList.Items.Count.Wait().EqualTo(0);
            ClaimStorage.Add( new []
            {
                Claim.CreateDefault() with{UserId = userId, Id = "1"},
                Claim.CreateDefault() with{UserId = userId, Id = "2"},
                Claim.CreateDefault() with{UserId = userId, Id = "3"}
            });
            vacationPage.Refresh();
            
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
            const string userId = "3";
            var vacationPage = Navigation.OpenEmployeeVacationListPage(userId);
            vacationPage.WaitLoaded();
            vacationPage.ClaimList.Items.Count.Wait().EqualTo(0);

            var firstClaimStartDate = GetStartDateFromNow(10);
            var firstClaimEndDate = GetEndDateFromNow(24);
            var secondClaimStartDate = GetStartDateFromNow(50);
            var secondClaimEndDate = GetEndDateFromNow(60);

            ClaimStorage.Add( new []
                {
                    Claim.CreateDefault() with
                    {
                        UserId = userId,
                        StartDate = firstClaimStartDate,
                        EndDate = firstClaimEndDate
                    },
                    Claim.CreateDefault() with
                    {
                        UserId = userId, 
                        Id = "2",
                        StartDate = secondClaimStartDate,
                        EndDate = secondClaimEndDate
                    },
                }
            );
            vacationPage.Refresh();

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
            const string userId = "4";
            var vacationPage = Navigation.OpenEmployeeVacationListPage(userId);
            vacationPage.WaitLoaded();
            vacationPage.ClaimList.Items.Count.Wait().EqualTo(0);

            var secondClaimStartDate = GetStartDateFromNow(50);
            var secondClaimEndDate = GetEndDateFromNow(64);
            ClaimStorage.Add(new []
            {
                Claim.CreateDefault() with
                {
                    UserId = userId
                },
                Claim.CreateDefault() with
                {
                    UserId = userId,
                    Id = "2",
                    StartDate = secondClaimStartDate,
                    EndDate = secondClaimEndDate
                }
            });
            vacationPage.Refresh();

            var chaim = vacationPage.ClaimList.Items.Wait().Single(
                element => element.TitleLink.Text,
                Is.EqualTo("Заявление 2")
            );

            var expectedPeriod = GetPeriod(secondClaimStartDate, secondClaimEndDate);
            chaim.PeriodLabel.Text.Wait().EqualTo(expectedPeriod);
        }
        
        private DateTime GetStartDateFromNow(int daysShift = 5)
        {
            return DateTime.Today.AddDays(daysShift);
        }

        private DateTime GetEndDateFromNow(int daysShift = 12)
        {
            return DateTime.Today.AddDays(daysShift);
        }

        private string GetPeriod(DateTime startDate, DateTime endDate)
        {
            return $@"{startDate.ToString(DateFormat)} - {endDate.ToString(DateFormat)}";
        }
    }
}