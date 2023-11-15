using Kontur.Selone.Elements;
using Kontur.Selone.Selectors.Context;
using SeloneCore;
using SeloneCore.Controls.BaseWebElements;

namespace VacationTests.PageObjects.Controls;

public class EmployeeClaimList : ControlBase
{
    public EmployeeClaimList(IContextBy contextBy, IPageObjectFactory pageObjectFactory) : base(contextBy, pageObjectFactory)
    {
        Items = pageObjectFactory.CreateElementsCollection<EmployeeClaimItem>(Container,"ClaimItem");
    }

    public ElementsCollection<EmployeeClaimItem> Items { get; }
}