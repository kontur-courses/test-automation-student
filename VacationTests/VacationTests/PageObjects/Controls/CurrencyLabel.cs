using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using SeloneCore;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Props;

namespace VacationTests.PageObjects.Controls;

public class CurrencyLabel : Label
{
    public CurrencyLabel(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
    }

    public IProp<decimal> Sum => Text.Currency();
}