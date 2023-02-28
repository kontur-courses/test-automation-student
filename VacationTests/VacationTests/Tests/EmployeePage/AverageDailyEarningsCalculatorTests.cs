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
            var page = Navigation
                .OpenPage<PageBase>(@"https://ronzhina.gitlab-pages.kontur.host/for-course/#/user/1")
                .WrappedDriver;

            Thread.Sleep(2000);
            var controlFactory = new ControlFactory();
            controlFactory.CreateControl<Button>(page.Search(x => x.WithTid("SalaryCalculatorTab"))).Click();

            controlFactory.CreateControl<Select>(page.Search(x => x.WithTid("first"))
                    .SearchContext.Search(x => x.WithTid("YearSelect")))
                .Visible.Wait().EqualTo(true);
            controlFactory.CreateControl<Input>(page.Search(x => x.WithTid("first"))
                    .SearchContext.Search(x => x.WithTid("SalaryCurrencyInput")))
                .Visible.Wait().EqualTo(true);

            controlFactory.CreateControl<Select>(page.Search(x => x.WithTid("second").WithTid("YearSelect")))
                .Visible.Wait().EqualTo(true);
            controlFactory.CreateControl<Input>(page.Search(x => x.WithTid("second").WithTid("SalaryCurrencyInput")))
                .Visible.Wait().EqualTo(true);

            var averageDailyEarningsCurrencyLabel =
                controlFactory.CreateControl<Label>(page.Search(x =>
                    x.WithTid("AverageDailyEarningsCurrencyLabel")));
            averageDailyEarningsCurrencyLabel.Visible.Wait().EqualTo(true);
            Assert.That(averageDailyEarningsCurrencyLabel.ToString(), Contains.Substring("370,85"));
        }
    }
}