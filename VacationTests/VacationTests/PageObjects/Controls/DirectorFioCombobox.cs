using Kontur.Selone.Elements;
using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using Kontur.Selone.Selectors.XPath;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;

namespace VacationTests.PageObjects.Controls;

public class DirectorFioCombobox : Combobox
{
    public DirectorFioCombobox(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
        MenuItems = controlFactory.CreateElementsCollection<DirectorItem>(Container.Root(),
            x => x.WithTid("ComboBoxMenu__item").FixedByIndex());
    }

    public new ElementsCollection<DirectorItem> MenuItems { get; private set; }
}