using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    [InjectControls]
    public class AverageDailyEarningsCalculatorPage : PageBase
    {
        public AverageDailyEarningsCalculatorPage(IWebDriver webDriver, ControlFactory controlFactory) : base(webDriver)
        {
            AverageSalaryRowControl = controlFactory.CreateControl<AverageSalaryRow>(webDriver.Search(x => x.WithTid("AverageSalaryRow")));
            SalaryCalculatorTab = controlFactory.CreateControl<Button>(
                webDriver.Search(x => x.WithTid("SalaryCalculatorTab")));
            FirstAverageSalaryRow = controlFactory.CreateControl<AverageSalaryRow>(
                webDriver.Search(x => x.WithTid("first")));
            SecondAverageSalaryRow = controlFactory.CreateControl<AverageSalaryRow>(
                webDriver.Search(x => x.WithTid("second")));
            AverageDailyEarningsCurrencyLabel = controlFactory.CreateControl<CurrencyLabel>(
                webDriver.Search(x => x.WithTid("AverageDailyEarningsCurrencyLabel")));
            ExcludedDaysInput = controlFactory.CreateControl<Input>(
                webDriver.Search(x => x.WithTid("CountOfExcludeDaysInput")));
            TotalEarningsCurrencyLabel = controlFactory.CreateControl<CurrencyLabel>(
                webDriver.Search(x => x.WithTid("TotalEarningsCurrencyLabel")));
        }
        public AverageSalaryRow AverageSalaryRowControl { get; }
        public AverageSalaryRow FirstAverageSalaryRow { get;}
        public AverageSalaryRow SecondAverageSalaryRow { get; }
        public CurrencyLabel TotalEarningsCurrencyLabel { get; }
        public Input ExcludedDaysInput { get; }
        public CurrencyLabel AverageDailyEarningsCurrencyLabel { get; }
        public Button SalaryCalculatorTab { get; }
    }
}
