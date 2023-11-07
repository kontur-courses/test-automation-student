using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    public class AverageDailyEarningsCalculatorPage : PageBase
    {
        public AverageDailyEarningsCalculatorPage(IWebDriver webDriver, ControlFactory controlFactory) : base(webDriver)
        {
            SalaryCalculatorTab =
                controlFactory.CreateControl<Button>(webDriver.Search(x => x.WithTid("SalaryCalculatorTab")));

            AverageSalaryRow1 =
                controlFactory.CreateControl<AverageSalaryRow>(webDriver.Search(x => x.WithTid("first")));

            AverageSalaryRow2 =
                controlFactory.CreateControl<AverageSalaryRow>(webDriver.Search(x => x.WithTid("second")));

            CountOfExcludeDaysInput = controlFactory.CreateControl<Input>(webDriver.Search(x =>
                x.WithTid("CountOfExcludeDaysInput")));

            DaysInTwoYearsLabel = controlFactory.CreateControl<Label>(webDriver.Search(x =>
                x.WithTid("DaysInTwoYearsLabel")));

            TotalDaysForCalcLabel = controlFactory.CreateControl<Label>(webDriver.Search(x =>
                x.WithTid("TotalDaysForCalcLabel")));

            TotalEarningsCurrencyLabel = controlFactory.CreateControl<CurrencyLabel>(webDriver.Search(x =>
                x.WithTid("TotalEarningsCurrencyLabel")));

            TotalDaysForCalcLabel = controlFactory.CreateControl<Label>(webDriver.Search(x =>
                x.WithTid("TotalDaysForCalcLabel")));

            AverageDailyEarningsCurrencyLabel =
                controlFactory.CreateControl<CurrencyLabel>(webDriver.Search(x =>
                    x.WithTid("AverageDailyEarningsCurrencyLabel")));
        }

        public Label DaysInTwoYearsLabel { get; }

        public Label TotalDaysForCalcLabel { get; }

        public AverageSalaryRow AverageSalaryRow2 { get; }

        public AverageSalaryRow AverageSalaryRow1 { get; }

        public CurrencyLabel TotalEarningsCurrencyLabel { get; }

        public Input CountOfExcludeDaysInput { get; }

        public CurrencyLabel AverageDailyEarningsCurrencyLabel { get; }

        public Button SalaryCalculatorTab { get; }

        public void WaitLoaded(int? timeout = null)
        {
            SalaryCalculatorTab.WaitPresence();
            AverageSalaryRow1.WaitPresence();
            AverageSalaryRow2.WaitPresence();
        }
    }
}