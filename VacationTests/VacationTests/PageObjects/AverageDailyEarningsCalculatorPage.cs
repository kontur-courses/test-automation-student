using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageObjects
{
    public class AverageDailyEarningsCalculatorPage : PageBase
    {
        [InjectControls]
        public AverageDailyEarningsCalculatorPage(IWebDriver webDriver, ControlFactory controlFactory) : base(webDriver)
        {
            SalaryCalculatorTab =
                controlFactory.CreateControl<Button>(webDriver.Search(x => x.WithTid("SalaryCalculatorTab")));

            AverageDailyEarningsCurrencyLabel = controlFactory.CreateControl<CurrencyLabel>(webDriver.Search(x =>
                x.WithTid("AverageDailyEarningsCurrencyLabel")));
            FirstRow = controlFactory.CreateControl<AverageSalaryRow>(webDriver.Search(x => x.WithTid("first")));
            SecondRow = controlFactory.CreateControl<AverageSalaryRow>(webDriver.Search(x => x.WithTid("second")));

            TotalEarningsCurrencyLabel =
                controlFactory.CreateControl<CurrencyLabel>(webDriver.Search(x =>
                    x.WithTid("TotalEarningsCurrencyLabel")));
            TotalDaysForCalcLabel = controlFactory.CreateControl<Label>(webDriver.Search(x => x.WithTid("TotalDaysForCalcLabel")));

            CountOfExcludeDaysInput =
                controlFactory.CreateControl<Input>(webDriver.Search(x => x.WithTid("CountOfExcludeDaysInput")));
        }

        public Label TotalDaysForCalcLabel { get; }
        public AverageSalaryRow SecondRow { get;  }
        public AverageSalaryRow FirstRow { get;  }
        public Button SalaryCalculatorTab { get; }
        public CurrencyLabel AverageDailyEarningsCurrencyLabel { get; }
        public CurrencyLabel TotalEarningsCurrencyLabel { get; }
        public Input CountOfExcludeDaysInput { get; }
    }
}