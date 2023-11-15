using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;

namespace SeloneCore.Controls.BaseWebElements;

public class Label : ControlBase
{
    public Label(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
    }

    public IProp<string> Text => Container.Text();
}