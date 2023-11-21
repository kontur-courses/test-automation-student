using System;
using System.Linq;
using Kontur.Selone.Properties;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.EmployeePage
{
    public class EmployeeVacationsListUiTests : VacationTestBase
    {
        readonly DateTime dateStartFirst = DateTime.Today.AddDays(7);
        readonly DateTime dateEndFirst =  DateTime.Today.AddDays(8);
        readonly DateTime dateStartSecond = DateTime.Today.AddDays(7);
        readonly DateTime dateEndSecond =  DateTime.Today.AddDays(8);

        [Test]
        public void CreateVacations_ShouldAddItemsToClaimsList()
        {
            var page = Navigation.OpenEmployeeVacationListPage(Guid.NewGuid().ToString());
            CreateClaimFromUI(page);
            CreateClaimFromUI(page);
            
            page.WaitLoaded();
            page.ClaimList.Items.Count.Wait().EqualTo(2);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitles_InRightOrder()
        {
            var page = Navigation.OpenEmployeeVacationListPage(Guid.NewGuid().ToString());
            
            CreateClaimFromUI(page);
            CreateClaimFromUI(page);
            CreateClaimFromUI(page);

            page.WaitLoaded();
            page.ClaimList.Items.Select(x => x.TitleLink.Text)
                .Wait().EqualTo(new []{"Заявление 1","Заявление 2","Заявление 3"});
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightTitleAndStatus_IgnoringOrder()
        {
            var page = Navigation.OpenEmployeeVacationListPage(Guid.NewGuid().ToString());
            
            CreateClaimFromUI(page);
            CreateClaimFromUI(page, dateStartSecond, dateEndSecond);
            
            var expected = new[]
            {
                ("Заявление 1", CreateStringPeriodDates(dateStartFirst, dateEndFirst), ClaimStatus.NonHandled.GetDescription()),
                ("Заявление 2", CreateStringPeriodDates(dateStartSecond, dateEndSecond), ClaimStatus.NonHandled.GetDescription())
            };
            
            page.WaitLoaded();
            page.ClaimList.Items.Select(x => Props.Create(x.TitleLink.Text, x.PeriodLabel.Text, x.StatusLabel.Text))
                .Wait().EquivalentTo(expected);
        }

        [Test]
        public void ClaimsList_ShouldDisplayRightPeriodForItem()
        {
            var page = Navigation.OpenEmployeeVacationListPage(Guid.NewGuid().ToString());
            
            CreateClaimFromUI(page);
            CreateClaimFromUI(page, dateStartSecond, dateEndSecond);
            
            page.WaitLoaded();
            var claim = page.ClaimList.Items.Wait().Single(x => x.TitleLink.Text, Is.EqualTo("Заявление 2"));
            claim.PeriodLabel.Text.Wait().EqualTo(CreateStringPeriodDates(dateStartSecond, dateEndSecond));
            claim.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
        }

        
        private void CreateClaimFromUI(EmployeeVacationListPage page, DateTime dateStart = default, DateTime dateEnd = default,
            ClaimType claimType = ClaimType.Child, string childAge = "3", string directorFio = "Голубев Александр Владимирович")
        {
            if (dateStart == default && dateEnd == default)//ничего лучше чем через иф не придумал
            {
                dateStart = dateStartFirst;
                dateEnd = dateEndFirst;
            }

            var claimCreationPage = page.CreateButton.ClickAndOpen<ClaimCreationPage>();
            claimCreationPage.ClaimTypeSelect.SelectValueByText(claimType.GetDescription());
            claimCreationPage.ChildAgeInput.ClearAndInputText(childAge);
            claimCreationPage.ClaimStartDatePicker.SetValue(dateStart);
            claimCreationPage.ClaimEndDatePicker.SetValue(dateEnd);
            claimCreationPage.DirectorFioCombobox.SelectValue(directorFio);

            claimCreationPage.SendButton.ClickAndOpen<EmployeeVacationListPage>();
        }
        
        public string CreateStringPeriodDates(DateTime startDate, DateTime endDate) => $"{startDate.ToString("dd.MM.yyyy")} - {endDate.ToString("dd.MM.yyyy")}";
        
        [Test]
        public void CreateClaimFromUIwew()
        {
            var dateStart = DateTime.Today.AddDays(7);
            var dateEnd =  DateTime.Today.AddDays(8);

            var page = Navigation.OpenEmployeeVacationListPage();
            page.WaitLoaded();
            page.ClaimList.Items.Count.Wait().EqualTo(0);

            var claimCreationPage = page.CreateButton.ClickAndOpen<ClaimCreationPage>();
            claimCreationPage.ClaimTypeSelect.SelectValueByText(ClaimType.Child.GetDescription());
            claimCreationPage.ChildAgeInput.ClearAndInputText("3");
            claimCreationPage.ClaimStartDatePicker.SetValue(dateStart);
            claimCreationPage.ClaimEndDatePicker.SetValue(dateEnd);
            claimCreationPage.DirectorFioCombobox.SelectValue("Голубев Александр Владимирович");

            page = claimCreationPage.SendButton.ClickAndOpen<EmployeeVacationListPage>();
            
            page.ClaimList.Items.Count.Wait().EqualTo(1);
            page.ClaimList.Items.Select(x => Props.Create(x.TitleLink.Text, x.PeriodLabel.Text, x.StatusLabel.Text))
                .Wait().EquivalentTo(new[] {("Заявление 1", $"{dateStart.ToString("dd.MM.yyyy")} - {dateEnd.ToString("dd.MM.yyyy")}", ClaimStatus.NonHandled.GetDescription())});
        }
        
        
    }
}