using System.Globalization;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using SeloneCore;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Props;

namespace VacationTests.PageObjects.Controls;

public class CurrencyInput : Input
{
    public CurrencyInput(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
    }

    public IProp<decimal> Sum => Value.Currency();

    public void ClearAndInputCurrency(decimal value)
    {
        ClearAndInputText(value.ToString(CultureInfo.InvariantCulture));
    }
}