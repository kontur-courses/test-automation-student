using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class DirectorItem : ControlBase
    {
        public DirectorItem(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy, controlFactory)
        {
        }

        public Label IdLabel { get; private set; }
        public Label FioLabel { get; private set; }
        public Label PositionLabel { get; private set; }
    }
}