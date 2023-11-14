using Kontur.Selone.Elements;
using Kontur.Selone.Selectors.Context;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;

namespace VacationTests.PageObjects.Controls;

public class EmployeeClaimList : ControlBase
{
    public EmployeeClaimList(IContextBy contextBy, IControlFactory controlFactory) : base(contextBy, controlFactory)
    {
        Items = controlFactory.CreateElementsCollection<EmployeeClaimItem>(Container,"ClaimItem");
    }

    public ElementsCollection<EmployeeClaimItem> Items { get; }
}