using System.ComponentModel;
using Kontur.RetryableAssertions.ValueProviding;
using Kontur.Selone.Elements;
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
            SalaryCalculatorTab = controlFactory.CreateControl<Button>(webDriver.Search(x => x.WithTid("SalaryCalculatorTab")));

            AverageSalaryFirstRow = controlFactory.CreateControl<AverageSalaryRow>(webDriver.Search(x => x.WithTid("first")));
            
            AverageSalarySecondRow = controlFactory.CreateControl<AverageSalaryRow>(webDriver.Search(x => x.WithTid("second")));
            
            CountOfExcludeDaysInput = controlFactory.CreateControl<CurrencyInput>(webDriver.Search(x =>
                x.WithTid("CountOfExcludeDaysInput")));
            
            TotalEarningsCurrencyLabel = controlFactory.CreateControl<CurrencyLabel>(webDriver.Search(x =>
                x.WithTid("TotalEarningsCurrencyLabel")));

            AverageDailyEarningsCurrencyLabel =
                controlFactory.CreateControl<CurrencyLabel>(webDriver.Search(x =>
                    x.WithTid("AverageDailyEarningsCurrencyLabel")));
        }

        public AverageSalaryRow AverageSalarySecondRow { get; }

        public AverageSalaryRow AverageSalaryFirstRow { get; }

        public CurrencyLabel TotalEarningsCurrencyLabel { get; }

        public CurrencyInput CountOfExcludeDaysInput { get; }

        public CurrencyLabel AverageDailyEarningsCurrencyLabel { get;}

        public Button SalaryCalculatorTab { get; }
        
        public void WaitLoaded(int? timeout = null)
        {
            SalaryCalculatorTab.WaitPresence();
            AverageSalaryFirstRow.WaitPresence();
            AverageSalarySecondRow.WaitPresence();
        }
    }
}