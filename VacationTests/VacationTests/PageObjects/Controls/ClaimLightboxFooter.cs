using Kontur.Selone.Selectors.Context;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;

namespace VacationTests.PageObjects.Controls;

public class ClaimLightboxFooter : ControlBase
{
    public ClaimLightboxFooter(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
    }

    public Button AcceptButton { get; private set; }
    public Button RejectButton { get; private set; }
}