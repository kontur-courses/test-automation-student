using Kontur.Selone.Pages;
using Kontur.Selone.Properties;
using NUnit.Framework;
using System;
using System.Linq;
using VacationTests.Claims;
using VacationTests.Infrastructure;

namespace VacationTests.Tests.EmployeePage
{
    public class EmployeeVacationsListTests : VacationTestBase
    {
        [Test]
        public void CreateVacations_ShouldAddItemsToClaimsList()
        {
            var userId = new Random().Next(1, 101).ToString();
            var page = Navigation.OpenEmployeeVacationListPage(userId);
            page.ClaimList.Items.Count.Wait().EqualTo(0);

            ClaimStorage.Add(new[] { 
                Claim.CreateChildType() with { UserId = userId } , 
                Claim.CreateChildType() with { UserId = userId } });
            page.Refresh();

            page.ClaimList.Items.Count.Wait().EqualTo(2);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitles_InRightOrder()
        {
            var userId = new Random().Next(1, 101).ToString();
            var page = Navigation.OpenEmployeeVacationListPage(userId);
            page.ClaimList.Items.Count.Wait().EqualTo(0);

            ClaimStorage.Add(new[] { 
                Claim.CreateChildType() with { UserId = userId },
                Claim.CreateChildType() with { UserId = userId },
                Claim.CreateChildType() with { UserId = userId }});
            page.Refresh();
            page.ClaimList.Items.Select(x => x.TitleLink.Text)
                .Wait().EqualTo(new[] { "Заявление 1", "Заявление 2", "Заявление 3" });
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitleAndStatus_IgnoringOrder()
        {
            var startDateFirstClaim = DateTime.Now.Date.AddDays(7);
            var endDateFirstClaim = DateTime.Now.Date.AddDays(15);

            var startDateSecondClaim = DateTime.Now.Date.AddDays(10);
            var endDateSecondClaim = DateTime.Now.Date.AddDays(20);
            
            var userId = new Random().Next(1, 101).ToString();
            var page = Navigation.OpenEmployeeVacationListPage(userId);
            page.ClaimList.Items.Count.Wait().EqualTo(0);

            ClaimStorage.Add(new[] { 
                Claim.CreateChildType() with { UserId = userId, StartDate = startDateFirstClaim, EndDate = endDateFirstClaim},
                Claim.CreateChildType() with { UserId = userId, StartDate = startDateSecondClaim, EndDate = endDateSecondClaim } });
            page.Refresh();
            page.ClaimList.Items.Count.Wait().EqualTo(2);

            var expected = new[]
                {
                    ("Заявление 2", startDateSecondClaim.ToString("dd.MM.yyyy") + " - " + endDateSecondClaim.ToString("dd.MM.yyyy"), true),
                    ("Заявление 1", startDateFirstClaim.ToString("dd.MM.yyyy") + " - " + endDateFirstClaim.ToString("dd.MM.yyyy"), true)
                };

            page.ClaimList.Items
                .Select(claim => Props.Create(claim.TitleLink.Text, claim.PeriodLabel.Text, claim.StatusLabel.Visible))
                .Wait().EquivalentTo(expected);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightPeriodForItem()
        {
            var startDateFirstClaim = DateTime.Now.Date.AddDays(7);
            var endDateFirstClaim = DateTime.Now.Date.AddDays(15);

            var startDateSecondClaim = DateTime.Now.Date.AddDays(10);
            var endDateSecondClaim = DateTime.Now.Date.AddDays(20);

            var userId = new Random().Next(1, 101).ToString();
            var page = Navigation.OpenEmployeeVacationListPage(userId);
            page.ClaimList.Items.Count.Wait().EqualTo(0);

            ClaimStorage.Add(new[] { 
                Claim.CreateChildType() with { UserId = userId, StartDate = startDateFirstClaim, EndDate = endDateFirstClaim},
                Claim.CreateChildType() with { UserId = userId, StartDate = startDateSecondClaim, EndDate = endDateSecondClaim } });
            page.Refresh();

            page.ClaimList.Items.Count.Wait().EqualTo(2);

            var claim = page.ClaimList.Items.Wait().Single(
                x => x.TitleLink.Text, Is.EqualTo("Заявление 2"));

            claim.PeriodLabel.Text.Wait().EqualTo(startDateSecondClaim.ToString("dd.MM.yyyy") + " - " + endDateSecondClaim.ToString("dd.MM.yyyy"));
            claim.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
        }
    }
}