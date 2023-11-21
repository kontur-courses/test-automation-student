using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;

namespace VacationTests.Infrastructure.PageElements
{
    public class AverageSalaryRow:ControlBase
    {
        public AverageSalaryRow(IContextBy contextBy) : base(contextBy)
        {
            var controlFactory = new ControlFactory();
            YearSelect = controlFactory.CreateControl<Select>(Container.Search(x => x.WithTid("YearSelect")));
            SalaryCurrencyInput =
                controlFactory.CreateControl<CurrencyInput>(Container.Search(x => x.WithTid("SalaryCurrencyInput")));
            CountBaseCurrencyLabel = controlFactory.CreateControl<CurrencyLabel>(Container.Search(x => x.WithTid("CountBaseCurrencyLabel")));
        }
        
        public Select YearSelect { get; }
        public CurrencyInput SalaryCurrencyInput { get; }
        public CurrencyLabel CountBaseCurrencyLabel { get; }
    }
}