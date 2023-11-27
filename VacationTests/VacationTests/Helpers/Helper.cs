using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Helpers
{
    public class Helper: VacationTestBase
    {
        public void CreateClaimFromUI(
            EmployeeVacationListPage page, Claim claim)
        {
            var claimCreationPage = page.CreateButton.ClickAndOpen<ClaimCreationPage>();
            claimCreationPage.ClaimTypeSelect.SelectValueByText(claim.Type.GetDescription());

            if (claim.ChildAgeInMonths != null)
            {
                var age = claim.ChildAgeInMonths / 12;
                claimCreationPage.ChildAgeInput.ClearAndInputText(age.ToString());
            }

            claimCreationPage.ClaimStartDatePicker.SetValue(claim.StartDate.ToString("dd.MM.yyyy"));
            claimCreationPage.ClaimEndDatePicker.SetValue(claim.EndDate.ToString("dd.MM.yyyy"));
            claimCreationPage.DirectorFioCombobox.SelectValue(claim.Director.Name);
            claimCreationPage.DirectorFioCombobox.WaitValue(claim.Director.Name);

            page = claimCreationPage.SendButton.ClickAndOpen<EmployeeVacationListPage>();
            page.WaitLoaded();
        }
    }
}
