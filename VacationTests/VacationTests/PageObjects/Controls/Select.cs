using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;
using SeloneCore;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Props;

namespace VacationTests.PageObjects.Controls;

public class Select : ControlBase
{
    public Select(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
    }

    public IProp<string> Value => Container.ReactValue();
    private Portal Portal => PageObjectFactory.CreateControl<Portal>(Container.Search(By.TagName("noscript")));

    // TODO: в параллели бывает нестабильность
    public void SelectValueByText(string text)
    {
        Container.Click();
        var itemSelector = $".//*[contains(@data-comp-name,'MenuItem CommonWrapper')][contains(.,'{text}')]";
        var item = PageObjectFactory.CreateControl<Button>(Portal.GetPortalElement().Search(By.XPath(itemSelector)));
        item.Visible.Wait().EqualTo(true);
        item.Click();
    }
}