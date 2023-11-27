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
            YearEarningsTableRowFirst = controlFactory.CreateControl<YearEarningsTableRow>(webDriver.Search(x => x.WithTid("first")));
            YearEarningsTableRowSecond = controlFactory.CreateControl<YearEarningsTableRow>(webDriver.Search(x => x.WithTid("second")));

            CountOfExcludeDaysInput =
                controlFactory.CreateControl<Input>(webDriver.Search(x => x.WithTid("CountOfExcludeDaysInput")));

            DaysInTwoYearsLabel =
                controlFactory.CreateControl<Label>(webDriver.Search(x => x.WithTid("DaysInTwoYearsLabel")));
            
            TotalEarningsCurrencyLabel = controlFactory.CreateControl<CurrencyLabel>(webDriver.Search(x =>
                x.WithTid("TotalEarningsCurrencyLabel")));
            
            TotalDaysForCalcLabel =
                controlFactory.CreateControl<Label>(webDriver.Search(x => x.WithTid("TotalDaysForCalcLabel")));
            
            AverageDailyEarningsCurrencyLabel =
                controlFactory.CreateControl<CurrencyLabel>(webDriver.Search(x =>
                    x.WithTid("AverageDailyEarningsCurrencyLabel")));
        }

        public YearEarningsTableRow YearEarningsTableRowFirst { get;}
        public YearEarningsTableRow YearEarningsTableRowSecond { get;}
        public Input CountOfExcludeDaysInput { get; }
        public Label DaysInTwoYearsLabel { get; }
        public CurrencyLabel TotalEarningsCurrencyLabel { get; }
        public Label TotalDaysForCalcLabel { get; }
        public CurrencyLabel AverageDailyEarningsCurrencyLabel { get; }
        
    }
}