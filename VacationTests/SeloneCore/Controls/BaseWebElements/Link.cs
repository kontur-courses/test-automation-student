using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;

namespace SeloneCore.Controls.BaseWebElements;

public class Link : ControlBase, ICanClickAndOpenPage
{
    public Link(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
    }

    public IProp<string> Text => Container.Text();

    public IPageObjectFactory PageObjectFactory => base.PageObjectFactory;
}