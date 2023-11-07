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
            // var page = Navigation
            //     .OpenPage<PageBase>(@"https://ronzhina.gitlab-pages.kontur.host/for-course/#/user/1")
            //     .WrappedDriver;

            var page = Navigation.OpenEmployeeVacationListPage();
            //ожидание страницы добавила в OpenEmployeeVacationListPage
            var calcPage = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
            calcPage.WaitLoaded();
            calcPage.AverageSalaryRow1.YearSelect.Visible.Wait().EqualTo(true);
            calcPage.AverageSalaryRow1.SalaryCurrencyInput.Visible.Wait().EqualTo(true);
            calcPage.AverageSalaryRow2.YearSelect.Visible.Wait().EqualTo(true);
            calcPage.AverageSalaryRow2.SalaryCurrencyInput.Visible.Wait().EqualTo(true);
            var averageDailyEarningsCurrency = calcPage.AverageDailyEarningsCurrencyLabel;
            averageDailyEarningsCurrency.Visible.Wait().EqualTo(true);

            Assert.That(averageDailyEarningsCurrency.ToString(), Contains.Substring("370,85"));
        }

        [Test]
        public void AverageEarningsCalculatorPage_AverageDailyEarningsTest()
        {
            var calcPage = OpenAverageDailyEarningsCalculatorPage();

            calcPage.AverageSalaryRow1.YearSelect.SelectValueByText("2020");
            calcPage.AverageSalaryRow2.YearSelect.SelectValueByText("2021");
            calcPage.AverageSalaryRow1.SalaryCurrencyInput.ClearAndInputText("100000");
            calcPage.AverageSalaryRow2.SalaryCurrencyInput.ClearAndInputText("200000");
            calcPage.CountOfExcludeDaysInput.ClearAndInputText("100");
            calcPage.AverageDailyEarningsCurrencyLabel.Sum.Wait().EqualTo(475.44m);
        }

        [Test]
        public void AverageEarningsCalculatorPage_EarningsMoreThanCountBaseTest()
        {
            var calcPage = OpenAverageDailyEarningsCalculatorPage();

            calcPage.AverageSalaryRow1.YearSelect.SelectValueByText("2020");
            calcPage.AverageSalaryRow1.SalaryCurrencyInput.ClearAndInputText("2000000,00");
            calcPage.AverageSalaryRow1.CountBaseCurrencyLabel.Sum.Wait().EqualTo(912000.00m);

            calcPage.AverageSalaryRow1.YearSelect.SelectValueByText("2021");
            calcPage.AverageSalaryRow1.CountBaseCurrencyLabel.Sum.Wait().EqualTo(966000.00m);
        }

        [Test]
        public void AverageEarningsCalculatorPage_EarningsLessThanCountBaseTest()
        {
            var salary1 = 100000.1m;
            var salary2 = 200000.2m;
            var calcPage = OpenAverageDailyEarningsCalculatorPage();

            calcPage.AverageSalaryRow1.SalaryCurrencyInput.ClearAndInputCurrency(salary1);
            calcPage.AverageSalaryRow2.SalaryCurrencyInput.ClearAndInputCurrency(salary2);

            calcPage.AverageSalaryRow1.CountBaseCurrencyLabel.Sum.Wait().EqualTo(salary1);
            calcPage.AverageSalaryRow2.CountBaseCurrencyLabel.Sum.Wait().EqualTo(salary2);
            calcPage.TotalEarningsCurrencyLabel.Sum.Wait().EqualTo(300000.3m);
        }

        [Test]
        public void AverageEarningsCalculatorPage_LeapYearTest()
        {
            var withLeapYearTwoYearsCountDays = "731";
            var withoutLeapYearTwoYearsCountDays = "730";
            var calcPage = OpenAverageDailyEarningsCalculatorPage();

            calcPage.AverageSalaryRow1.YearSelect.SelectValueByText("2020");
            calcPage.AverageSalaryRow2.YearSelect.SelectValueByText("2021");

            calcPage.TotalDaysForCalcLabel.Text.Wait().EqualTo(withLeapYearTwoYearsCountDays);
            calcPage.DaysInTwoYearsLabel.Text.Wait().EqualTo(withLeapYearTwoYearsCountDays);

            //хочется проверить, что в вычислениях среднего заработка тоже используется правильное значение
            calcPage.AverageSalaryRow1.SalaryCurrencyInput.ClearAndInputCurrency(600000m);
            calcPage.AverageSalaryRow2.SalaryCurrencyInput.ClearAndInputCurrency(700000m);

            calcPage.AverageDailyEarningsCurrencyLabel.Sum.Wait().EqualTo(1778.39m);

            calcPage.AverageSalaryRow1.YearSelect.SelectValueByText("2019");

            calcPage.TotalDaysForCalcLabel.Text.Wait().EqualTo(withoutLeapYearTwoYearsCountDays);
            calcPage.DaysInTwoYearsLabel.Text.Wait().EqualTo(withoutLeapYearTwoYearsCountDays);
            calcPage.AverageDailyEarningsCurrencyLabel.Sum.Wait().EqualTo(1780.82m);
        }

        [Test]
        public void AverageEarningsCalculatorPage_LeapYearTest3()
        {
            var withLeapYearTwoYearsCountDays = "731";
            var calcPage = OpenAverageDailyEarningsCalculatorPage();

            calcPage.AverageSalaryRow1.YearSelect.SelectValueByText("2020");
            calcPage.AverageSalaryRow2.YearSelect.SelectValueByText("2021");

            calcPage.TotalDaysForCalcLabel.Text.Wait().EqualTo(withLeapYearTwoYearsCountDays);
            calcPage.DaysInTwoYearsLabel.Text.Wait().EqualTo(withLeapYearTwoYearsCountDays);

            //хочется проверить, что в вычислениях среднего заработка тоже используется правильное значение
            calcPage.AverageSalaryRow1.SalaryCurrencyInput.ClearAndInputCurrency(600000m);
            calcPage.AverageSalaryRow2.SalaryCurrencyInput.ClearAndInputCurrency(700000m);

            calcPage.AverageDailyEarningsCurrencyLabel.Sum.Wait().EqualTo(1778.39m);

            calcPage.CountOfExcludeDaysInput.ClearAndInputText("100");

            calcPage.DaysInTwoYearsLabel.Text.Wait().EqualTo(withLeapYearTwoYearsCountDays);
            calcPage.TotalDaysForCalcLabel.Text.Wait().EqualTo("631");
            calcPage.AverageDailyEarningsCurrencyLabel.Sum.Wait().EqualTo(2060.22m);
        }

        private AverageDailyEarningsCalculatorPage OpenAverageDailyEarningsCalculatorPage()
        {
            var page = Navigation.OpenEmployeeVacationListPage();
            var calcPage = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
            calcPage.WaitLoaded();
            return calcPage;
        }
    }
}