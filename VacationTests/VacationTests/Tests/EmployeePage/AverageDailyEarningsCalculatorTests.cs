using System.Threading;
using Kontur.Selone.Extensions;
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
            calcPage.AverageSalaryFirstRow.YearSelect.Visible.Wait().EqualTo(true);
            calcPage.AverageSalaryFirstRow.SalaryCurrencyInput.Visible.Wait().EqualTo(true);
            calcPage.AverageSalarySecondRow.YearSelect.Visible.Wait().EqualTo(true);
            calcPage.AverageSalarySecondRow.SalaryCurrencyInput.Visible.Wait().EqualTo(true);
            var averageDailyEarningsCurrency = calcPage.AverageDailyEarningsCurrencyLabel;
            averageDailyEarningsCurrency.Visible.Wait().EqualTo(true);

            Assert.That(averageDailyEarningsCurrency.ToString(), Contains.Substring("370,85"));
        }

        [Test]
        public void AverageDailyEarningsCalculatorPage_AverageDailyEarningsTest()
        {
            var calcPage = OpenAverageDailyEarningsCalculatorPage();

            calcPage.AverageSalaryFirstRow.YearSelect.SelectValueByText("2020");
            calcPage.AverageSalarySecondRow.YearSelect.SelectValueByText("2021");
            calcPage.AverageSalaryFirstRow.SalaryCurrencyInput.ClearAndInputText("100000");
            calcPage.AverageSalarySecondRow.SalaryCurrencyInput.ClearAndInputText("200000");
            calcPage.CountOfExcludeDaysInput.ClearAndInputText("100");
            calcPage.AverageDailyEarningsCurrencyLabel.Sum.Wait().EqualTo(475.44m);
        }

        [Test]
        public void AverageDailyEarningsCalculatorPage_OverCountBaseTest()
        {
            var calcPage = OpenAverageDailyEarningsCalculatorPage();

            calcPage.AverageSalaryFirstRow.YearSelect.SelectValueByText("2020");
            calcPage.AverageSalaryFirstRow.SalaryCurrencyInput.ClearAndInputText("2000000,00");
            calcPage.AverageSalaryFirstRow.CountBaseCurrencyLabel.Sum.Wait().EqualTo(912000.00m);

            calcPage.AverageSalaryFirstRow.YearSelect.SelectValueByText("2021");
            calcPage.AverageSalaryFirstRow.CountBaseCurrencyLabel.Sum.Wait().EqualTo(966000.00m);
        }

        [Test]
        public void AverageDailyEarningsCalculatorPage_OverCountBaseTest345()
        {
            var salary1 = 100000.1m;
            var salary2 = 200000.2m;
            var calcPage = OpenAverageDailyEarningsCalculatorPage();

            calcPage.AverageSalaryFirstRow.SalaryCurrencyInput.ClearAndInputCurrency(salary1);
            calcPage.AverageSalarySecondRow.SalaryCurrencyInput.ClearAndInputCurrency(salary2);
            calcPage.AverageSalaryFirstRow.CountBaseCurrencyLabel.Sum.Wait().EqualTo(salary1);
            calcPage.AverageSalarySecondRow.CountBaseCurrencyLabel.Sum.Wait().EqualTo(salary2);

            calcPage.TotalEarningsCurrencyLabel.Sum.Wait().EqualTo(300000.3m);
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