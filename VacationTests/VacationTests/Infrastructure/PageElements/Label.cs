using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;

namespace VacationTests.Infrastructure.PageElements
{
    public class Label : ControlBase
    {
        public Label(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy, controlFactory)
        {
        }

        public IProp<string> Text => Container.Text();
    }
}