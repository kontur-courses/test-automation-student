using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using SeloneCore.Props;

namespace SeloneCore.Controls.BaseWebElements;

public class Toggle : ControlBase
{
    public Toggle(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
    }

    public IProp<string> Text => Container.Text();
    public IProp<bool> Checked => Container.Checked();
}