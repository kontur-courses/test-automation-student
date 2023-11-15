using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using SeloneCore;
using SeloneCore.Controls.BaseWebElements;

namespace VacationTests.PageObjects.Controls;

// Класс элемента списка отпусков EmployeeClaimList наследуем от ControlBase,
// поскольку это тоже контрол и могут понадобиться базовые методы и пропсы
public class EmployeeClaimItem : ControlBase
{
    public EmployeeClaimItem(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
        TitleLink = pageObjectFactory.CreateControl<Link>(Container, "TitleLink");
        PeriodLabel = pageObjectFactory.CreateControl<Label>(Container, "PeriodLabel");
        StatusLabel = pageObjectFactory.CreateControl<Label>(Container, "StatusLabel");
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