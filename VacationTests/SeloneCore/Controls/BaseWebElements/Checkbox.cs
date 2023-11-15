using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using SeloneCore.Props;

namespace SeloneCore.Controls.BaseWebElements;

public class Checkbox : ControlBase
{
    public Checkbox(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
    }

    public IProp<string> Text => Container.Text();
    public IProp<bool> Checked => Container.Checked();

    public void SetChecked()
    {
        Checked.Wait().EqualTo(false);
        Click();
        Checked.Wait().EqualTo(true);
    }

    public void SetUnchecked()
    {
        Checked.Wait().EqualTo(true);
        Click();
        Checked.Wait().EqualTo(false);
    }
}