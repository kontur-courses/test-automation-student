using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;

namespace VacationTests.PageObjects.Controls;

public class ClaimLightbox : ControlBase
{
    public ClaimLightbox(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
    }

    public Portal ClaimModal { get; private set; }

    // Вариант работы с элементами, для которых нужен ленивый поиск
    public Label ModalHeaderLabel => CreateControlByTid<Label>("ModalHeader");

    public Button CrossButton => CreateControlByTid<Button>("modal-close");

    public Label StatusLabel => CreateControlByTid<Label>("StatusLabel");

    public Label ClaimTypeLabel => CreateControlByTid<Label>("ClaimTypeLabel");

    public Label ChildAgeLabel => CreateControlByTid<Label>("ChildAgeLabel");

    public Label PeriodLabel => CreateControlByTid<Label>("PeriodLabel");

    public Label AvailableDaysMessageLabel => CreateControlByTid<Label>("AvailableDaysMessageLabel");

    public Label AvailableDaysLabel => CreateControlByTid<Label>("AvailableDaysLabel");

    public Checkbox PayNowCheckbox => CreateControlByTid<Checkbox>("PayNowCheckbox");

    public Label DirectorFioLabel => CreateControlByTid<Label>("DirectorFioLabel");

    public ClaimLightboxFooter Footer => CreateControlByTid<ClaimLightboxFooter>("ModalFooter");

    private TControl CreateControlByTid<TControl>(string tid) where TControl : ControlBase
    {
        return ControlFactory.CreateControl<TControl>(GetModalContext().Search(x => SelectorExtensions.WithTid((ByDummy) x, tid)));
    }

    private IWebElement GetModalContext()
    {
        return ClaimModal.GetPortalElement();
    }
}