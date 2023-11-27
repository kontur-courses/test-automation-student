using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    [InjectControls]
    public class AverageSalaryRow : ControlBase
    {
        public AverageSalaryRow(IContextBy contextBy, ControlFactory controlFactory  ) : base(contextBy)
        {
            YearSelect = controlFactory.CreateControl<Select>(Container.Search(x => x.WithTid("YearSelect")));
            CountBaseCurrencyLabel = controlFactory.CreateControl<CurrencyLabel>(Container.Search(x => x.WithTid("CountBaseCurrencyLabel")));
            SalaryCurrencyInput = controlFactory.CreateControl<Input>(Container.Search(x => x.WithTid("SalaryCurrencyInput")));
        }
        public CurrencyLabel CountBaseCurrencyLabel { get; private set; }
        public Select YearSelect { get; private set; }
        public Input SalaryCurrencyInput { get; private set; }

    }
}
