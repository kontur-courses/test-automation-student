using Kontur.Selone.Elements;
using Kontur.Selone.Selectors.Context;
using Kontur.Selone.Selectors.XPath;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class EmployeeClaimList : ControlBase
    {
        public EmployeeClaimList(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy, controlFactory)
        {
            Items = FindCollectionByTid<EmployeeClaimItem>("ClaimItem");
        }

        public ElementsCollection<EmployeeClaimItem> Items { get; }
    }
}