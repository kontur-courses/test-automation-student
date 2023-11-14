using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Props;

namespace VacationTests.PageObjects.Controls;

public class CurrencyLabel : Label
{
    public CurrencyLabel(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
    }

    public IProp<decimal> Sum => Text.Currency();
}