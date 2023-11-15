using Kontur.Selone.Selectors.Context;
using SeloneCore;
using SeloneCore.Controls.BaseWebElements;

namespace VacationTests.PageObjects.Controls;

public class ClaimLightboxFooter : ControlBase
{
    public ClaimLightboxFooter(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
    }

    public Button AcceptButton { get; private set; }
    public Button RejectButton { get; private set; }
}