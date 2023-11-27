using System;
using System.Linq;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.EmployeePage
{
    public class EmployeeVacationsListUiTests2 : VacationTestBase
    {
        
        
        [Test]
        public void CreateClaimFromUI()
        {
            var vacationPage = Navigation.OpenEmployeeVacationListPage();
            vacationPage.WaitLoaded();
            vacationPage.ClaimList.Items.Count.Wait().EqualTo(0);

            var createVacationPage = vacationPage.CreateButton.ClickAndOpen<ClaimCreationPage>();
            
            var nowDate = DateTime.Now;
            var startDate = nowDate.AddDays(7);
            var endDate = nowDate.AddDays(21);
            createVacationPage.ClaimTypeSelect.SelectValueByText(ClaimType.Child.GetDescription());
            createVacationPage.ChildAgeInput.ClearAndInputText("7");
            createVacationPage.ClaimStartDatePicker.SetValue(startDate);
            createVacationPage.ClaimEndDatePicker.SetValue(endDate);
            createVacationPage.DirectorFioCombobox.SelectValue("Захаров Максим Николаевич");
            vacationPage = createVacationPage.SendButton.ClickAndOpen<EmployeeVacationListPage>();
            vacationPage.WaitLoaded();
            vacationPage.ClaimList.Items.Count.Wait().EqualTo(1);
            
            var chaim = vacationPage.ClaimList.Items.Single();
            chaim.TitleLink.Text.Wait().EqualTo("Заявление 1");
            chaim.PeriodLabel.Text.Wait().EqualTo(GetPeriod(startDate, endDate));
            chaim.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
        }
        
        private string GetPeriod(DateTime startDate, DateTime endDate)
        {
            const string dateFormat = "dd.MM.yyyy";
            return $@"{startDate.ToString(dateFormat)} - {endDate.ToString(dateFormat)}";
        }
    }
}