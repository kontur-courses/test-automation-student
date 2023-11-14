using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;

namespace VacationTests.PageObjects.Controls;

// Класс элемента списка отпусков EmployeeClaimList наследуем от ControlBase,
// поскольку это тоже контрол и могут понадобиться базовые методы и пропсы
public class EmployeeClaimItem : ControlBase
{
    public EmployeeClaimItem(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
        TitleLink = controlFactory.CreateControl<Link>(Container, "TitleLink");
        PeriodLabel = controlFactory.CreateControl<Label>(Container, "PeriodLabel");
        StatusLabel = controlFactory.CreateControl<Label>(Container, "StatusLabel");
    }

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
}