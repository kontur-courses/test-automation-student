using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageNavigation;

// Классы с тестами запускаются параллельно.
// Тесты внутри одного класса проходят последовательно.
[assembly: Parallelizable(ParallelScope.All)]
[assembly: LevelOfParallelism(4)]
namespace VacationTests
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            MyBrowserPool.Dispose();
        }
    }
    public abstract class VacationTestBase
    {
        protected IWebDriver WebDriver => MyBrowserPool.Get();
        protected ClaimStorage ClaimStorage => new(LocalStorage);
        protected LocalStorage LocalStorage => new(WebDriver);
        private ControlFactory ControlFactory => new(LocalStorage, ClaimStorage);
        protected Navigation Navigation => new(WebDriver, ControlFactory);
        private Screenshoter Screenshoter => new(WebDriver);

        protected WebDriverPool Pool;

        [OneTimeSetUp]
        public void SetUp()
        {
            //var browserType = Environment.GetEnvironmentVariable("TEST_BROWSER") ?? "firefox";
            //IWebDriverFactory factory;

            //switch (browserType)
            //{
            //    case "chrome":
            //        factory = new ChromeDriverFactory();
            //        break;
            //    case "firefox":
            //        factory = new FirefoxDriverFactory();
            //        break;
            //    //case "safari":
            //    //    factory = new SafariDriverFactory();
            //    //    break;
            //    default:
            //        factory = new FirefoxDriverFactory();
            //        break;
            //}

            //var delegateWebDriverCleaner = new DelegateWebDriverCleaner(x => x.ResetWindows());
            //Pool = new WebDriverPool(factory, delegateWebDriverCleaner);
        }
        [TearDown]
        protected virtual void TearDown()
        {
            Screenshoter.SaveTestFailureScreenshot();
            MyBrowserPool.Release();
        }
    }
}