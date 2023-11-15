using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;

namespace SeloneCore.Controls.BaseWebElements;

public abstract class Lightbox
{
    protected readonly ISearchContext Container;
    protected readonly IPageObjectFactory PageObjectFactory;
    protected Lightbox(IContextBy contextBy, IPageObjectFactory pageObjectFactory)
    {
        Container = contextBy.SearchContext.SearchElement(contextBy.By);
        PageObjectFactory = pageObjectFactory;
    }
}