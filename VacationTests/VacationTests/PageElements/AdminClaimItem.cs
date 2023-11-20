using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    [InjectControls]
    public class AdminClaimItem : ControlBase
    {
        public AdminClaimItem(IContextBy contextBy) : base(contextBy)
        {
        }

        [ByTid("ListItemCheckbox")] public Checkbox ItemCheckbox { get; private set; }

        public Button RejectButton { get; private set; }

        public Button AcceptButton { get; private set; }

        public Label UserFioLabel { get; private set; }

        public Button TitleLink { get; private set; }
        public Label PeriodLabel { get; private set; }
        public Label StatusLabel { get; private set; }

        public void MouseOver()
        {
            Container.Mouseover();
        }
    }
}