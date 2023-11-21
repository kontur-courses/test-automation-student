using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Selone.Pages;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Data;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.AdminPage
{
    public class AdminVacationsListTests: VacationTestBase
    {
        [Test]
        public void CheckList_ShouldShowsOtherEmployeesByUI()
        {
            var page = Navigation.OpenEmployeeVacationListPage();
            page.PageScenario.CreateClaimFromUI();
            page.PageScenario.CreateClaimFromUI();

            page.Refresh();
            page.ClaimList.Items.Count.Wait().EqualTo(2);
        }
        
        //task 1
        [Test]
        public void CheckList_ShouldShowsDifferentEmployees()
        {
            var page = Navigation.OpenAdminVacationListPage();
            var claim1 = Claim.CreateDefault() with { UserId = "1", Director = Directors.SuperDirector};
            var claim2 = Claim.CreateDefault() with { UserId = "2" };
            ClaimStorage.Add(new[]{claim1, claim2});

            page.Refresh();
            page.ClaimList.Items.Count.Wait().EqualTo(2);
        }
        
        //task 2
        [Test]
        public void CreateClaimsWithDifferentStatuses_CheckStatusesAndShowButtons()
        {
            var page = Navigation.OpenAdminVacationListPage();
            var claim1 = Claim.CreateDefault() with { UserId = "1" , Status = ClaimStatus.NonHandled};
            var claim2 = Claim.CreateDefault() with { UserId = "2" , Status = ClaimStatus.Accepted};
            var claim3 = Claim.CreateDefault() with { UserId = "3" , Status = ClaimStatus.Rejected};
            ClaimStorage.Add(new []{claim1, claim2, claim3});

            page.Refresh();
            var items = page.ClaimList.Items.Select(x => 
                (x.StatusLabel.Text.Get(),x.AcceptButton.Present.Get(), x.RejectButton.Present.Get()));
            items.Wait().EquivalentTo(new []
            {
                (ClaimStatus.NonHandled.GetDescription(), true, true),
                (ClaimStatus.Accepted.GetDescription(), false, false),
                (ClaimStatus.Rejected.GetDescription(), false, false)
            });
        }
        
        //task 3
        [Test]
        public void CreateClaim_CheckShowsData()
        {
            var page = Navigation.OpenAdminVacationListPage();
            page.ClaimList.Items.Count.Wait().EqualTo(0);

            var claim1 = Claim.CreateDefault();
            ClaimStorage.Add(new []{claim1});

            page.Refresh();
            var item = page.ClaimList.Items.Select(x => x.GetString()).Single();
            item.Should().Be(
                $"Заявление {claim1.Id}{claim1.Status.GetDescription()}{claim1.StartDate.ToString("dd.MM.yyyy")} - {claim1.EndDate.ToString("dd.MM.yyyy")}Иванов Петр Семенович");
        }

        private static IEnumerable<TestCaseData> getDataTestCases()
        {
            yield return new TestCaseData(new Func<EmployeeClaimItem, Button>(x=> x.AcceptButton), ClaimStatus.Accepted);
            yield return new TestCaseData(new Func<EmployeeClaimItem, Button>(x=> x.RejectButton), ClaimStatus.Rejected);
        }
        //task4-5
        [TestCaseSource(nameof(getDataTestCases))]
        public void ClaimActionButtonsInList_CheckStatuses(Func<EmployeeClaimItem, Button> getButton, ClaimStatus claimStatus )
        {
            var page = Navigation.OpenAdminVacationListPage();
            var claim1 = Claim.CreateDefault();
            ClaimStorage.Add(new []{claim1});

            page.Refresh();
            var item = page.ClaimList.Items.Single(x=> x.TitleLink.Text.Get() == $"Заявление {claim1.Id}");
            getButton(item).Click();
            item.StatusLabel.Text.Wait().EqualTo(claimStatus.GetDescription());
        }
        
        private static IEnumerable<TestCaseData> getDataLightboxTestCases()
        {
            yield return new TestCaseData(new Func<ClaimLightboxFooter, Button>(x=> x.AcceptButton), ClaimStatus.Accepted);
            yield return new TestCaseData(new Func<ClaimLightboxFooter, Button>(x=> x.RejectButton), ClaimStatus.Rejected);
        }
        //task4-5 через лайтбокс
        [TestCaseSource(nameof(getDataLightboxTestCases))]
        public void ClaimActionButtonsInLightbox_CheckStatuses(Func<ClaimLightboxFooter, Button> getButton, ClaimStatus claimStatus)
        {
            var page = Navigation.OpenAdminVacationListPage();
            var claim1 = Claim.CreateDefault();
            ClaimStorage.Add(new[] { claim1 });

            page.Refresh();
            var item = page.ClaimList.Items.Single(x => x.TitleLink.Text.Get() == $"Заявление {claim1.Id}");
            var claimLightbox = item.TitleLink.ClickAndOpen<ClaimLightbox>();
            getButton(claimLightbox.Footer).Click();
            item.StatusLabel.Text.Wait().EqualTo(claimStatus.GetDescription());
        }

        //task6
        [Test]
        public void CheckSortClaimList()
        {
            var page = Navigation.OpenAdminVacationListPage();
            var claim1 = Claim.CreateDefault() with { Id = "2"};
            var claim2 = Claim.CreateDefault() with { Id = "1"};
            ClaimStorage.Add(new[]{claim1, claim2});

            page.Refresh();
            page.ClaimList.Items.Count.Wait().EqualTo(2);
            var items = page.ClaimList.Items.Select(x => x.TitleLink.Text.Get());
            items.Wait().EqualTo(new []{$"Заявление {claim1.Id}", $"Заявление {claim2.Id}"});

        }
    }
}