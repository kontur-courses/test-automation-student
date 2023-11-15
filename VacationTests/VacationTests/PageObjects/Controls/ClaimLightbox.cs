using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;
using SeloneCore;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;

namespace VacationTests.PageObjects.Controls;

public class ClaimLightbox : Lightbox
{
    public ClaimLightbox(IContextBy contextBy, IPageObjectFactory pageObjectFactory)
        : base(contextBy.SearchContext.Search(x=>x.WithTid("ClaimLightbox")), pageObjectFactory)
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
        => PageObjectFactory.CreateControl<TControl>(GetModalContext().Search(x => x.WithTid(tid)));

    private IWebElement GetModalContext()
    {
        return ClaimModal.GetPortalElement();
    }
}