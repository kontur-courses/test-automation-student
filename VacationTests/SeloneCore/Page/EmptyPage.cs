using Kontur.Selone.Selectors.Context;

namespace SeloneCore.Page;

public class EmptyPage : PageBase
{
    public EmptyPage(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
    }
}