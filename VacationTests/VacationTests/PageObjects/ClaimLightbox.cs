using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    public class ClaimLightbox : PageBase
    {
        private readonly ControlFactory controlFactory;

        public ClaimLightbox(IWebDriver webDriver, ControlFactory controlFactory) : base(webDriver)
        {
            this.controlFactory = controlFactory;
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

        private TControl CreateControlByTid<TControl>(string tid)
        {
            return controlFactory.CreateControl<TControl>(GetModalContext().Search(x => x.WithTid(tid)));
        }

        private IWebElement GetModalContext()
        {
            return ClaimModal.GetPortalElement();
        }
    }
}