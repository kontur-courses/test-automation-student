using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class PageFooter : ControlBase
    {
        public PageFooter(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy, controlFactory)
        {
        }

        public Link KnowEnvironmentLink { get; private set; }
        public Link OurFooterLink { get; private set; }
    }
}