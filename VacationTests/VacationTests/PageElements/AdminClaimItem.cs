using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    [InjectControls]
    public class AdminClaimItem : ControlBase
    {
        public AdminClaimItem(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy)
        {
            ItemCheckbox = controlFactory.CreateControl<Checkbox>(Container.Search(x => x.WithTid("ListItemCheckbox")));
            TitleLink = controlFactory.CreateControl<Button>(Container.Search(x => x.WithTid("TitleLink")));
            UserFioLabel = controlFactory.CreateControl<Label>(Container.Search(x => x.WithTid("UserFioLabel")));
            PeriodLabel = controlFactory.CreateControl<Label>(Container.Search(x => x.WithTid("PeriodLabel")));
            StatusLabel = controlFactory.CreateControl<Label>(Container.Search(x => x.WithTid("StatusLabel")));
            AcceptButton = controlFactory.CreateControl<Button>(Container.Search(x => x.WithTid("AcceptButton")));
            RejectButton = controlFactory.CreateControl<Button>(Container.Search(x => x.WithTid("RejectButton")));
        }

        public Checkbox ItemCheckbox { get; }

        public Button RejectButton { get; }

        public Button AcceptButton { get; }

        public Label UserFioLabel { get; }

        public Button TitleLink { get; }
        public Label PeriodLabel { get; }
        public Label StatusLabel { get; }

        public void MouseOver()
        {
            Container.Mouseover();
        }
    }
}