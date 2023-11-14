using Kontur.Selone.Selectors.Context;

namespace SeloneCore.Controls.BaseWebElements;

public class SomeWebElement : ControlBase
{
    public SomeWebElement(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
    }
}