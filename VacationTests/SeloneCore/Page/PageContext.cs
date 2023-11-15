using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;

namespace SeloneCore.Page;

public class PageContext : IContextBy
{
    public PageContext(ISearchContext searchContext)
    {
        SearchContext = searchContext;
        By = null;
    }

    public ISearchContext SearchContext { get; }
    public By By { get; }
}