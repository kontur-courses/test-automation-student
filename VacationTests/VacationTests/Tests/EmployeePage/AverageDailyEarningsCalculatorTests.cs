using NUnit.Framework;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.EmployeePage
{
    public class AverageDailyEarningsCalculatorTests : VacationTestBase
    {

        [Test, Description("Смок-Тест")]
        public void SmokyTest()
        {
            var page = Init();
            var firstRow = page.YearEarningsTableRowFirst;
            firstRow.YearSelect.Visible.Wait().EqualTo(true);
            firstRow.SalaryCurrencyInput.Visible.Wait().EqualTo(true);
            
            var secondRow = page.YearEarningsTableRowSecond; 
            secondRow.YearSelect.Visible.Wait().EqualTo(true);
            secondRow.SalaryCurrencyInput.Visible.Wait().EqualTo(true);

            var averageDailyEarningsCurrencyLabel = page.AverageDailyEarningsCurrencyLabel;
            averageDailyEarningsCurrencyLabel.Visible.Wait().EqualTo(true);

            averageDailyEarningsCurrencyLabel.Sum.Wait().EqualTo(370.85m);
        }

        [Test, Description("При заполнении всех полей, среднедневной заработок считается корректно")]
        public void CheckAverageDailyValue()
        {
            var page = Init();
            var firstRow = page.YearEarningsTableRowFirst;
            firstRow.YearSelect.SelectValueByText("2020");
            firstRow.SalaryCurrencyInput.ClearAndInputCurrency(100000m);
            
            var secondRow = page.YearEarningsTableRowSecond;
            secondRow.YearSelect.SelectValueByText("2021");
            secondRow.SalaryCurrencyInput.ClearAndInputCurrency(200000m);

            page.CountOfExcludeDaysInput.Visible.Wait().EqualTo(true);
            page.CountOfExcludeDaysInput.ClearAndInputText("100");

            page.AverageDailyEarningsCurrencyLabel.Sum.Wait().EqualTo(471.44m);
        }

        [Test, Description("Если заработок за год больше базы для расчёта, то база для расчёта берется по умолчанию из года")]
        public void CheckDefaultBaseValueWhenSalaryMoreBase()
        {
            var page = Init();
            var firstRow = page.YearEarningsTableRowFirst;
            firstRow.YearSelect.SelectValueByText("2020");
            firstRow.SalaryCurrencyInput.ClearAndInputCurrency(2000000m);
            
            firstRow.CountBaseCurrencyLabel.Sum.Wait().EqualTo(912000m);
        }
        
        [Test, Description("Если заработок за год МЕНЬШЕ базы для расчёта, то база для расчёта == заработку")]
        public void CheckEqualBaseAndSalaryWhenSalaryLessBase()
        {
            var page = Init();
            page.YearEarningsTableRowFirst.SalaryCurrencyInput.ClearAndInputCurrency(100000.1m);
            page.YearEarningsTableRowSecond.SalaryCurrencyInput.ClearAndInputCurrency(200000.2m);

            page.YearEarningsTableRowFirst.CountBaseCurrencyLabel.Sum.Wait().EqualTo(100000.1m);
            page.YearEarningsTableRowSecond.CountBaseCurrencyLabel.Sum.Wait().EqualTo(200000.2m);
            page.TotalEarningsCurrencyLabel.Sum.Wait().EqualTo(300000.3m);
        }

        [Test, Description("При выборе високосного года количество дней для расчёта считается корректно")]
        public void CheckCalculationWithLeapYear()
        {
            var page = Init();
            page.YearEarningsTableRowFirst.YearSelect.SelectValueByText("2020");
            page.YearEarningsTableRowSecond.YearSelect.SelectValueByText("2021");
            page.TotalDaysForCalcLabel.Text.Wait().EqualTo("731");
            
            page.YearEarningsTableRowFirst.YearSelect.SelectValueByText("2019");
            page.TotalDaysForCalcLabel.Text.Wait().EqualTo("730");
        }

        [Test, Description("При указании исключённых дней, они должны корректно исключаться из расчёта")]
        public void CheckCalculationWithExcludedDays()
        {
            var page = Init();
            page.YearEarningsTableRowFirst.YearSelect.SelectValueByText("2020");
            page.YearEarningsTableRowSecond.YearSelect.SelectValueByText("2021");
            page.DaysInTwoYearsLabel.Text.Wait().EqualTo("731");
            
            page.CountOfExcludeDaysInput.ClearAndInputText("100");
            page.TotalDaysForCalcLabel.Text.Wait().EqualTo("631");
        }

        private AverageDailyEarningsCalculatorPage Init()
        {
            var page = Navigation.OpenEmployeeVacationListPage();
            page.SalaryCalculatorTab.Visible.Wait().EqualTo(true);
            return page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
        }
    }
}