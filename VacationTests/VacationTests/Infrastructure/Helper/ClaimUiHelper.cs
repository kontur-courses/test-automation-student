using System;
using VacationTests.Claims;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Infrastructure.Helper
{
    public class ClaimUiHelper
    {
        public EmployeeVacationListPage CreateClaimFromUi(EmployeeVacationListPage employeePage,
            DateTime? claimStartDate = null, DateTime? claimEndDate = null, ClaimType claimType = ClaimType.Paid,
            string director = "Захаров",
            string? childAge = "1")
        {
            var claimCreationPage = employeePage.CreateButton.ClickAndOpen<ClaimCreationPage>();
            claimCreationPage.ClaimTypeSelect.SelectValueByText(claimType.GetDescription());
            if (claimType == ClaimType.Child)
                claimCreationPage.ChildAgeInput.ClearAndInputText(childAge);
            var startDate = claimStartDate ?? DateTime.Today.AddDays(5);
            var endDate = claimEndDate ?? startDate.AddDays(1);
            claimCreationPage.ClaimStartDatePicker.SetValue(startDate);
            claimCreationPage.ClaimEndDatePicker.SetValue(endDate);
            claimCreationPage.DirectorFioCombobox.SelectValue(director);
            employeePage = claimCreationPage.SendButton.ClickAndOpen<EmployeeVacationListPage>();
            return employeePage;
        }
    }
}