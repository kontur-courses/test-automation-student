using Kontur.Selone.Selectors.Context;
using SeloneCore.Controls;

namespace SeloneCore.Page;

public class EmptyPage : PageBase
{
    public EmptyPage(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
    }
}