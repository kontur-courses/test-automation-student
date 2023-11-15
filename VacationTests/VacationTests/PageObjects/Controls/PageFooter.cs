using Kontur.Selone.Selectors.Context;
using SeloneCore;
using SeloneCore.Controls.BaseWebElements;

namespace VacationTests.PageObjects.Controls;

public class PageFooter : ControlBase
{
    public PageFooter(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
    }

    public Link KnowEnvironmentLink { get; private set; }
    public Link OurFooterLink { get; private set; }
}