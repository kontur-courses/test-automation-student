using Kontur.Selone.Selectors.Context;
using SeloneCore;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Page;
using VacationTests.PageObjects.Controls;

namespace VacationTests.PageObjects.Pages;

public class ClaimCreationPage : PageBase
{
    public ClaimCreationPage(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
    }

    public Label UserFioLabel { get; private set; }
    public Select ClaimTypeSelect { get; private set; }
    public Input ChildAgeInput { get; private set; }
    public DatePicker ClaimStartDatePicker { get; private set; }
    public DatePicker ClaimEndDatePicker { get; private set; }
    public Label AvailableDaysMessage { get; private set; }
    public Label AvailableDaysLabel { get; private set; }
    public Checkbox PayNowCheckbox { get; private set; }
    public DirectorFioCombobox DirectorFioCombobox { get; private set; }
    public Button SendButton { get; private set; }
}