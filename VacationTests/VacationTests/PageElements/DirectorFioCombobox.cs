using Kontur.Selone.Elements;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class DirectorFioCombobox : Combobox
    {
        public DirectorFioCombobox(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy, controlFactory)
        {
            MenuItems = FindCollectionByTid<DirectorItem>("ComboBoxMenu__item", SearchArea.Page);
        }

        public new ElementsCollection<DirectorItem> MenuItems { get; private set; }
    }
}