using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class ClaimLightboxFooter : ControlBase
    {
        public ClaimLightboxFooter(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy, controlFactory)
        {
        }

        public Button AcceptButton { get; private set; }
        public Button RejectButton { get; private set; }
    }
}