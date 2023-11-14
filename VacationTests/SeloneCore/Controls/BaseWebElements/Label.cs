using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;

namespace SeloneCore.Controls.BaseWebElements;

public class Label : ControlBase
{
    public Label(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
    }

    public IProp<string> Text => Container.Text();
}