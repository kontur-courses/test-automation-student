using Kontur.Selone.Selectors.Context;

namespace SeloneCore.Controls.BaseWebElements;

public class SomeWebElement : ControlBase
{
    public SomeWebElement(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
    }
}