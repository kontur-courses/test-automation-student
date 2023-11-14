using Kontur.Selone.Selectors.Context;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;

namespace VacationTests.PageObjects.Controls;

public class DirectorItem : ControlBase
{
    public DirectorItem(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
    }

    public Label IdLabel { get; private set; }
    public Label FioLabel { get; private set; }
    public Label PositionLabel { get; private set; }
}