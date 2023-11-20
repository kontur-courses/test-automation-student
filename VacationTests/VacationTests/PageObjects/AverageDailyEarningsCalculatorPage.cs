using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    [InjectControls]
    public class AverageDailyEarningsCalculatorPage : PageBase
    {
        public AverageDailyEarningsCalculatorPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public Label DaysInTwoYearsLabel { get; private set; }

        public Label TotalDaysForCalcLabel { get; private set; }

        [ByTid("second")] public AverageSalaryRow AverageSalaryRow2 { get; private set; }

        [ByTid("first")] public AverageSalaryRow AverageSalaryRow1 { get; private set; }

        public CurrencyLabel TotalEarningsCurrencyLabel { get; private set; }

        public Input CountOfExcludeDaysInput { get; private set; }

        public CurrencyLabel AverageDailyEarningsCurrencyLabel { get; private set; }

        public Button SalaryCalculatorTab { get; private set; }

        public void WaitLoaded(int? timeout = null)
        {
            SalaryCalculatorTab.WaitPresence();
            AverageSalaryRow1.WaitPresence();
            AverageSalaryRow2.WaitPresence();
        }
    }
}