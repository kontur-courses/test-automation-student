using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    [InjectControls]
    public class AverageSalaryRow : ControlBase
    {
        public AverageSalaryRow(IContextBy contextBy) : base(contextBy)
        {
        }

        public CurrencyLabel CountBaseCurrencyLabel { get; private set; }

        public CurrencyInput SalaryCurrencyInput { get; private set; }

        public Select YearSelect { get; private set; }
    }
}