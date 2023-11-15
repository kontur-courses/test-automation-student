using Kontur.Selone.Elements;
using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using Kontur.Selone.Selectors.XPath;
using SeloneCore;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;

namespace VacationTests.PageObjects.Controls;

public class DirectorFioCombobox : Combobox
{
    public DirectorFioCombobox(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
        MenuItems = pageObjectFactory.CreateElementsCollection<DirectorItem>(Container.Root(),
            x => x.WithTid("ComboBoxMenu__item").FixedByIndex());
    }

    public new ElementsCollection<DirectorItem> MenuItems { get; private set; }
}