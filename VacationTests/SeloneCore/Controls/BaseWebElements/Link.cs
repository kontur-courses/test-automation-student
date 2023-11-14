using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;

namespace SeloneCore.Controls.BaseWebElements;

public class Link : ControlBase, ICanClickAndOpenPage
{
    public Link(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
    }

    public IProp<string> Text => Container.Text();

    public IControlFactory ControlFactory => base.ControlFactory;
}