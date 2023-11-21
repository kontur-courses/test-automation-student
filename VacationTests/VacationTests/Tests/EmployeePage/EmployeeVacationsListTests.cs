using System;
using System.Linq;
using Kontur.Selone.Pages;
using Kontur.Selone.Properties;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;

namespace VacationTests.Tests.EmployeePage
{
    public class EmployeeVacationsListTests: VacationTestBase
    {
        readonly DateTime dateStartFirst = DateTime.Today.Date.AddDays(7);
        readonly DateTime dateEndFirst =  DateTime.Today.Date.AddDays(8);
        readonly DateTime dateStartSecond = DateTime.Today.Date.AddDays(10);
        readonly DateTime dateEndSecond =  DateTime.Today.Date.AddDays(11);
        private string employeeId;

        [SetUp]
        public void SetUp()
        {
            employeeId = Guid.NewGuid().ToString();
        }

        [Test]
        public void CreateVacations_ShouldAddItemsToClaimsList()
        {
            var page = Navigation.OpenEmployeeVacationListPage(employeeId);
            var claim1 = Claim.CreateDefault() with{UserId = employeeId};
            var claim2 = Claim.CreateDefault() with{UserId = employeeId};
            ClaimStorage.Add(new []{claim1,claim2});

            page.Refresh();
            page.WaitLoaded();
            page.ClaimList.Items.Count.Wait().EqualTo(2);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitles_InRightOrder()
        {
            var page = Navigation.OpenEmployeeVacationListPage(employeeId);
            var claim1 = Claim.CreateDefault() with {UserId = employeeId, EndDate = dateEndFirst};
            var claim2 = Claim.CreateDefault() with {UserId = employeeId, EndDate = dateEndFirst};
            var claim3 = Claim.CreateDefault() with {UserId = employeeId, EndDate = dateEndFirst};
            ClaimStorage.Add(new []{claim1,claim2, claim3});

            page.Refresh();
            page.WaitLoaded();
            page.ClaimList.Items.Select(x => x.TitleLink.Text)
                .Wait().EqualTo(new []{"Заявление 1","Заявление 2","Заявление 3"});
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitleAndStatus_IgnoringOrder()
        {
            var page = Navigation.OpenEmployeeVacationListPage(employeeId);
            var claim1 = Claim.CreateDefault() with { UserId = employeeId, EndDate = dateEndFirst };
            var claim2 = Claim.CreateDefault() with { UserId = employeeId, StartDate = dateStartSecond, EndDate = dateEndSecond };
            ClaimStorage.Add(new []{claim1,claim2});
            
            var expected = new[]
            {
                ("Заявление 1", CreateStringPeriodDates(dateStartFirst, dateEndFirst), ClaimStatus.NonHandled.GetDescription()),
                ("Заявление 2", CreateStringPeriodDates(dateStartSecond, dateEndSecond), ClaimStatus.NonHandled.GetDescription())
            };

            page.Refresh();
            page.WaitLoaded();
            page.ClaimList.Items.Count.Wait().EqualTo(2);
            page.ClaimList.Items.Select(x => Props.Create(x.TitleLink.Text, x.PeriodLabel.Text, x.StatusLabel.Text)).Wait().EquivalentTo(expected);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightPeriodForItem()
        {
            var page = Navigation.OpenEmployeeVacationListPage(employeeId);
            var claim1 = Claim.CreateDefault() with { UserId = employeeId, EndDate = dateEndFirst };
            var claim2 = Claim.CreateDefault() with { UserId = employeeId, StartDate = dateStartSecond, EndDate = dateEndSecond };
            ClaimStorage.Add(new []{claim1,claim2});

            page.Refresh();
            page.WaitLoaded();
            var claim = page.ClaimList.Items.Wait().Single(x => x.TitleLink.Text, Is.EqualTo("Заявление 2"));
            claim.PeriodLabel.Text.Wait().EqualTo(CreateStringPeriodDates(dateStartSecond, dateEndSecond));
            claim.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
        }
        
        public string CreateStringPeriodDates(DateTime startDate, DateTime endDate) => $"{startDate.ToString("dd.MM.yyyy")} - {endDate.ToString("dd.MM.yyyy")}";
    }
}