using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;
using SeloneCore.Props;

namespace SeloneCore.Controls.BaseWebElements;

public abstract class ControlBase : IHaveContainer
{
    protected IPageObjectFactory PageObjectFactory { get; }
    public IWebElement Container { get; }

    protected ControlBase(IContextBy contextBy, IPageObjectFactory pageObjectFactory)
    {
        PageObjectFactory = pageObjectFactory;
        Container = contextBy.SearchContext.SearchElement(contextBy.By);
    }

    public IProp<bool> Present => Container.Present(); // Typo IsPreset. Expression reflection
    public IProp<bool> Visible => Container.Visible();
    public IProp<bool> Disabled => Container.Disabled();

    public void Click()
    {
        Container.Click();
    }

    public override string ToString()
    {
        try
        {
            return $"{Container.TagName} {Container.Text}";
        }
        catch (StaleElementReferenceException)
        {
            return "StaleElement (not found in DOM)";
        }
    }
}