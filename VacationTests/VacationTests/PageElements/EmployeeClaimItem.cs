using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.PageElements
{
    [InjectControls]
    // Класс элемента списка отпусков EmployeeClaimList наследуем от ControlBase,
    // поскольку это тоже контрол и могут понадобиться базовые методы и пропсы
    public class EmployeeClaimItem : ControlBase
    {
        public EmployeeClaimItem(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy)
        {
            TitleLink = controlFactory.CreateControl<Link>(Container.Search(x => x.WithTid("TitleLink")));
            UserFioLabel = controlFactory.CreateControl<Label>(Container.Search(x => x.WithTid("UserFioLabel")));
            PeriodLabel = controlFactory.CreateControl<Label>(Container.Search(x => x.WithTid("PeriodLabel")));
            StatusLabel = controlFactory.CreateControl<Label>(Container.Search(x => x.WithTid("StatusLabel")));
            AcceptButton = controlFactory.CreateControl<Button>(Container.Search(x => x.WithTid("AcceptButton")));
            RejectButton = controlFactory.CreateControl<Button>(Container.Search(x => x.WithTid("RejectButton")));
        }

        public Label UserFioLabel { get;}
        public Button RejectButton { get;}
        public Button AcceptButton { get;}

        // При обращении из теста к любому элементу списка отпусков будут доступны три свойства
        public Link TitleLink { get; }
        public Label PeriodLabel { get; }
        public Label StatusLabel { get; }

        // Можно вот так реализовать метод для наведения мыши на конкретный элемент,
        // такой метод может понадобиться, если только при наведении на элемент списка показывается какой-то контрол
        public void MouseOver()
        {
            Container.Mouseover();
        }
        public string GetString() => $"{TitleLink.Text.Get()}{StatusLabel.Text.Get()}{PeriodLabel.Text.Get()}{UserFioLabel.Text.Get()}";
    }
}