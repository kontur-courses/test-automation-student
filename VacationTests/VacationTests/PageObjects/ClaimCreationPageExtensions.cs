using VacationTests.Claims;
using VacationTests.Infrastructure;

namespace VacationTests.PageObjects
{
    [InjectControls]
    public static class ClaimCreationPageExtensions
    {
        public static void CreateClaimFromUI(this ClaimCreationPage page, Claim claim)
        {
            page.ClaimTypeSelect.SelectValueByText(claim.Type.GetDescription());
            if (claim.Type == ClaimType.Child) page.ChildAgeInput.ClearAndInputText(claim.ChildAgeInMonths.ToString());
            page.ClaimStartDatePicker.SetValue(claim.StartDate);
            page.ClaimEndDatePicker.SetValue(claim.EndDate);
            page.DirectorFioCombobox.SelectValue(claim.Director.Name);
        }
    }
}