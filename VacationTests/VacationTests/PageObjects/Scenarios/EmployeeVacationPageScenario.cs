using System;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageObjects.Scenarios
{
    public class EmployeeVacationPageScenario
    {
        [InjectControls]
        private EmployeeVacationListPage page;

        public EmployeeVacationPageScenario(EmployeeVacationListPage page)
        {
            this.page = page;
        }

        public void CreateClaimFromUI(DateTime dateStart = default, DateTime dateEnd = default,
            ClaimType claimType = ClaimType.Child, string childAge = "3", string directorFio = "Голубев Александр Владимирович")
        {
            if (dateStart == default && dateEnd == default)//ничего лучше чем через иф не придумал
            {
                dateStart = DateTime.Today.AddDays(7);
                dateEnd = DateTime.Today.AddDays(8);
            }

            var claimCreationPage = page.CreateButton.ClickAndOpen<ClaimCreationPage>();
            claimCreationPage.ClaimTypeSelect.SelectValueByText(claimType.GetDescription());
            claimCreationPage.ChildAgeInput.ClearAndInputText(childAge);
            claimCreationPage.ClaimStartDatePicker.SetValue(dateStart);
            claimCreationPage.ClaimEndDatePicker.SetValue(dateEnd);
            claimCreationPage.DirectorFioCombobox.SelectValue(directorFio);

            claimCreationPage.SendButton.ClickAndOpen<EmployeeVacationListPage>();
        }
    }
}