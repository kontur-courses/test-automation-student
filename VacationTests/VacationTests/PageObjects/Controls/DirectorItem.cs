using Kontur.Selone.Selectors.Context;
using SeloneCore;
using SeloneCore.Controls.BaseWebElements;

namespace VacationTests.PageObjects.Controls;

public class DirectorItem : ControlBase
{
    public DirectorItem(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
    }

    public Label IdLabel { get; private set; }
    public Label FioLabel { get; private set; }
    public Label PositionLabel { get; private set; }
}