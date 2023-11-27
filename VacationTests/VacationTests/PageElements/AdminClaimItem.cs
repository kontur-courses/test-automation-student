using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    // Класс элемента списка отпусков EmployeeClaimList наследуем от ControlBase,
    // поскольку это тоже контрол и могут понадобиться базовые методы и пропсы
    [InjectControls]
    public class AdminClaimItem : ControlBase
    {
        public AdminClaimItem(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy)
        {
            ListItemCheckbox =
                controlFactory.CreateControl<Checkbox>(Container.Search(x => x.WithTid("ListItemCheckbox")));
            TitleLink = controlFactory.CreateControl<Link>(Container.Search(x => x.WithTid("TitleLink")));
            UserFioLabel = controlFactory.CreateControl<Label>(Container.Search(x => x.WithTid("UserFioLabel")));
            PeriodLabel = controlFactory.CreateControl<Label>(Container.Search(x => x.WithTid("PeriodLabel")));
            StatusLabel = controlFactory.CreateControl<Label>(Container.Search(x => x.WithTid("StatusLabel")));
            AcceptButton = controlFactory.CreateControl<Button>(Container.Search(x => x.WithTid("AcceptButton")));
            RejectButton = controlFactory.CreateControl<Button>(Container.Search(x => x.WithTid("RejectButton")));
        }

        // При обращении из теста к любому элементу списка отпусков будут доступны три свойства
        public Checkbox ListItemCheckbox { get; }
        public Link TitleLink { get; }
        public Label UserFioLabel { get; }
        public Label PeriodLabel { get; }
        public Label StatusLabel { get; }
        public Button AcceptButton { get; }
        public Button RejectButton { get; }
        

        // Можно вот так реализовать метод для наведения мыши на конкретный элемент,
        // такой метод может понадобиться, если только при наведении на элемент списка показывается какой-то контрол
        public void MouseOver()
        {
            Container.Mouseover();
        }

        public bool IsApproveControlPresent()
        {
            return AcceptButton.Present.Get() && RejectButton.Present.Get();
        }
    }
}