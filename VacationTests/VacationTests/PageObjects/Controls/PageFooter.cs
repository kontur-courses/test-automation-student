using Kontur.Selone.Selectors.Context;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;

namespace VacationTests.PageObjects.Controls;

public class PageFooter : ControlBase
{
    public PageFooter(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
    }

    public Link KnowEnvironmentLink { get; private set; }
    public Link OurFooterLink { get; private set; }
}