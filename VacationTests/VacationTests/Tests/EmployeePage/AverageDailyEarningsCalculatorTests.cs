using System.Threading;
using FluentAssertions;
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
            var page = Navigation.OpenEmployeeVacationListPage();
            var averagePage = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
            
            averagePage.FirstRow.YearSelect.Visible.Wait().EqualTo(true);
            averagePage.FirstRow.SalaryCurrencyInput.Visible.Wait().EqualTo(true);
            averagePage.SecondRow.YearSelect.Visible.Wait().EqualTo(true);
            averagePage.SecondRow.SalaryCurrencyInput.Visible.Wait().EqualTo(true);
            averagePage.AverageDailyEarningsCurrencyLabel.Visible.Wait().EqualTo(true);
            averagePage.AverageDailyEarningsCurrencyLabel.Sum.Wait().EqualTo(370.85m);
        }
        
        // Сценарий 1. При заполнении всех полей, среднедневной заработок считается корректно.
        //
        // Зайти на страницу «Калькулятор среднего заработка»
        // Выбрать год 2020 в «Расчётный год» для первой строчки
        // Выбрать год 2021 в «Расчётный год» для второй строчки
        // В первый год в заработке указать 100000
        // Во второй 200000
        // Указать количество исключаемых дней 100
        // Проверить что среднедневной заработок 475.44
        
         [Test]
         public void AverageDailyEarningsCurrencyCalculation()
         {
             var page = Navigation.OpenEmployeeVacationListPage();
             var earningsCalculatorPage = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
             earningsCalculatorPage.FirstRow.YearSelect.SelectValueByText("2020");
             earningsCalculatorPage.SecondRow.YearSelect.SelectValueByText("2021");
             
             earningsCalculatorPage.FirstRow.SalaryCurrencyInput.ClearAndInputCurrency(100000m);
             earningsCalculatorPage.SecondRow.SalaryCurrencyInput.ClearAndInputCurrency(200000m);
             earningsCalculatorPage.CountOfExcludeDaysInput.ClearAndInputText("100");
             
             earningsCalculatorPage.AverageDailyEarningsCurrencyLabel.Sum.Wait().EqualTo(475.44m);
         }
         
         // Сценарий 2. Если заработок за год больше базы для расчёта, то база для расчёта берется по умолчанию из года.
         //
         // Зайти на страницу «Калькулятор среднего заработка»
         // Выбрать год 2020 в «Расчётный год» для первой строчки
         // В заработке указать 2000000,00
         // Проверяем, что база для расчёта должна быть 912 000
         // Перевыбрать год на 2021
         // Проверить, что База для расчёта должна стать 966 000
        
         [Test]
         public void CountBaseCurrencyCalculation_OverLimit()
         {
             var page = Navigation.OpenEmployeeVacationListPage();
             var earningsCalculatorPage = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
             
             earningsCalculatorPage.FirstRow.YearSelect.SelectValueByText("2020");
             earningsCalculatorPage.FirstRow.SalaryCurrencyInput.ClearAndInputCurrency(2000000m);
             
             earningsCalculatorPage.FirstRow.CountBaseCurrencyLabel.Sum.Wait().EqualTo(912000m);
             
             earningsCalculatorPage.FirstRow.YearSelect.SelectValueByText("2021");
             
             earningsCalculatorPage.FirstRow.CountBaseCurrencyLabel.Sum.Wait().EqualTo(966000.00m);
         }
         
         // Сценарий 3. Если заработок за год МЕНЬШЕ базы для расчёта, то база для расчёта == заработку.
         //     
         // Зайти на страницу «Калькулятор среднего заработка»
         // В первый год в заработке указать 100000,1
         // Во второй 200000,2
         // Проверить, что база для расчёта равна заданным значениям
         // Проверить, что в формуле сумма годов равна 300 000,3
          
         [Test]
         public void CountBaseCurrencyCalculation_LessLimit()
         {
             var page = Navigation.OpenEmployeeVacationListPage();
             var earningsCalculatorPage = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
             
             earningsCalculatorPage.FirstRow.SalaryCurrencyInput.ClearAndInputCurrency(100000.1m);
             earningsCalculatorPage.SecondRow.SalaryCurrencyInput.ClearAndInputCurrency(200000.2m);

             earningsCalculatorPage.FirstRow.CountBaseCurrencyLabel.Sum.Wait().EqualTo(100000.1m);
             earningsCalculatorPage.SecondRow.CountBaseCurrencyLabel.Sum.Wait().EqualTo(200000.2m);
             earningsCalculatorPage.TotalEarningsCurrencyLabel.Sum.Wait().EqualTo(300000.3m);
         }
         
         // Сценарий 4.* При выборе високосного года количество дней для расчёта считается корректно.
         //
         // Зайти на страницу «Калькулятор среднего заработка»
         // Выбрать год 2020 в «Расчётный год» для первой строчки
         // Выбрать год 2021 в «Расчётный год» для второй строчки
         // Проверить, что количество дней в формуле 731
         // Поменять год с 2020 на 2019
         // Проверить, что количество дней в формуле 730
         
         [Test]
         public void TotalDaysCalculation_LeapYear()
         {
             var page = Navigation.OpenEmployeeVacationListPage();
             var earningsCalculatorPage = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
             
             earningsCalculatorPage.FirstRow.YearSelect.SelectValueByText("2020");
             earningsCalculatorPage.SecondRow.YearSelect.SelectValueByText("2021");

             earningsCalculatorPage.TotalDaysForCalcLabel.Text.Wait().EqualTo("731");
             
             earningsCalculatorPage.FirstRow.YearSelect.SelectValueByText("2019");
             
             earningsCalculatorPage.TotalDaysForCalcLabel.Text.Wait().EqualTo("730");
         }
         
         // Сценарий 5.* При указании исключённых дней, они должны корректно исключаться из расчёта.
         //
         // Зайти на страницу «Калькулятор среднего заработка»
         // Выбрать год 2020 в «Расчётный год» для первой строчки
         // Выбрать год 2021 в «Расчётный год» для второй строчки
         // Проверить, что количество дней, которые можно вычесть 731
         // Указать количество исключаемых дней 100
         // Проверить, что количество дней в формуле 631
         
         [Test]
         public void TotalDaysCalculation_ExcludeDays()
         {
             var page = Navigation.OpenEmployeeVacationListPage();
             var earningsCalculatorPage = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
             
             earningsCalculatorPage.FirstRow.YearSelect.SelectValueByText("2020");
             earningsCalculatorPage.SecondRow.YearSelect.SelectValueByText("2021");
             
             earningsCalculatorPage.TotalDaysForCalcLabel.Text.Wait().EqualTo("731");
             
             earningsCalculatorPage.CountOfExcludeDaysInput.ClearAndInputText("100");
             
             earningsCalculatorPage.TotalDaysForCalcLabel.Text.Wait().EqualTo("631");
         }
    }
}