using NUnit.Framework;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.EmployeePage
{
    public class AverageDailyEarningsCalculatorTests : VacationTestBase
    {
        [Test]
        public void SmokyTest()
        {
            var page = Navigation.OpenEmployeeVacationListPage();
            var SalaryTab = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();

            SalaryTab.FirstAverageSalaryRow.YearSelect.Visible.Wait().EqualTo(true);
            SalaryTab.FirstAverageSalaryRow.SalaryCurrencyInput.Visible.Wait().EqualTo(true);
            SalaryTab.SecondAverageSalaryRow.YearSelect.Visible.Wait().EqualTo(true);
            SalaryTab.SecondAverageSalaryRow.SalaryCurrencyInput.Visible.Wait().EqualTo(true);
            SalaryTab.AverageDailyEarningsCurrencyLabel.Visible.Wait().EqualTo(true);

            Assert.That(SalaryTab.AverageDailyEarningsCurrencyLabel.ToString(), Contains.Substring("370,85"));
        }

        [Test]
        public void AverageSalary_Calculate_Success()
        {
            var page = Navigation.OpenEmployeeVacationListPage();
            var SalaryTab = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
            SalaryTab.FirstAverageSalaryRow.YearSelect.SelectValueByText("2020");
            SalaryTab.SecondAverageSalaryRow.YearSelect.SelectValueByText("2021");

            SalaryTab.FirstAverageSalaryRow.SalaryCurrencyInput.ClearAndInputText("100000");
            SalaryTab.SecondAverageSalaryRow.SalaryCurrencyInput.ClearAndInputText("200000");

            SalaryTab.ExcludedDaysInput.ClearAndInputText("100");

            SalaryTab.AverageDailyEarningsCurrencyLabel.Sum.Wait().EqualTo(475.44m);
        }

        [Test]
        public void BaseForCalculation_IsTakenFromDefaultFromTheYear()
        {
            var page = Navigation.OpenEmployeeVacationListPage();
            var SalaryTab = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();

            SalaryTab.FirstAverageSalaryRow.YearSelect.SelectValueByText("2020");
            SalaryTab.FirstAverageSalaryRow.SalaryCurrencyInput.ClearAndInputText("2000000");
            SalaryTab.FirstAverageSalaryRow.CountBaseCurrencyLabel.Sum.Wait().EqualTo(912000.00m);
            SalaryTab.FirstAverageSalaryRow.YearSelect.SelectValueByText("2021");
            SalaryTab.FirstAverageSalaryRow.CountBaseCurrencyLabel.Sum.Wait().EqualTo(966000.00m);
        }

        [Test]
        public void BaseForCalculation_EarningsForTheYearAreLessThanBase()
        {
            var page = Navigation.OpenEmployeeVacationListPage();
            var SalaryTab = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();

            SalaryTab.FirstAverageSalaryRow.SalaryCurrencyInput.ClearAndInputText("100000,1");
            SalaryTab.SecondAverageSalaryRow.SalaryCurrencyInput.ClearAndInputText("200000,2");

            SalaryTab.TotalEarningsCurrencyLabel.Sum.Wait().EqualTo(300000.3m);
        }
    }
}