using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    public class AdminVacationListPage : PageBase
    {
        public AdminVacationListPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public Label TitleLabel { get; private set; }
        public Link ClaimsTab { get; private set; }
        public Button DownloadButton { get; private set; }
        public PageFooter Footer { get; private set; }

        public void AssertThatCalculatorTabNotExist()
        {
            // TODO pe: нужен какой-то простой способ создать глобальный контрол, имея только WebDriver и ТИД
            var salaryCalculatorTab = new SomeWebElement(WrappedDriver.Search(x => x.WithTid("ClaimList")));
            salaryCalculatorTab.Present.Wait().EqualTo(false);
        }
        
        public void AssertThatCreateButtonNotExist()
        {
            var createButton = new SomeWebElement(WrappedDriver.Search(x => x.WithTid("ClaimList")));
            createButton.Present.Wait().EqualTo(false);
        }
    }
}