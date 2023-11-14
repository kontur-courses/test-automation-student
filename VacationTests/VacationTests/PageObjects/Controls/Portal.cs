using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Props;

namespace VacationTests.PageObjects.Controls;

public class Portal : ControlBase
{
    public Portal(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
    }

    public IWebElement GetPortalElement()
    {
        Present.Wait().EqualTo(true);
        var renderContainerId = Container.GetAttribute("data-render-container-id");
        try
        {
            return Container.Root()
                .SearchElement(By.CssSelector($"[data-rendered-container-id='{renderContainerId}']"));
        }
        catch (NoSuchElementException)
        {
            return null;
        }
    }
}