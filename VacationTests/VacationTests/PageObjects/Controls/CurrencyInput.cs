using System.Globalization;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Props;

namespace VacationTests.PageObjects.Controls;

public class CurrencyInput : Input
{
    public CurrencyInput(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
    }

    public IProp<decimal> Sum => Value.Currency();

    public void ClearAndInputCurrency(decimal value)
    {
        ClearAndInputText(value.ToString(CultureInfo.InvariantCulture));
    }
}