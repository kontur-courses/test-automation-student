using System;
using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using NUnit.Framework;
using OpenQA.Selenium;
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
    public abstract class VacationTestBase
    {
        protected IWebDriver WebDriver => MyBrowserPool.Get();
        protected ClaimStorage ClaimStorage => new(LocalStorage);
        protected LocalStorage LocalStorage => new(WebDriver);
        private ControlFactory ControlFactory => new(LocalStorage, ClaimStorage);
        protected Navigation Navigation => new(WebDriver, ControlFactory);
        private Screenshoter Screenshoter => new(WebDriver); 

        
        
        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            MyBrowserPool.Dispose();
            WebDriver.Close();
            WebDriver.Quit();
        }

        [TearDown]
        public void TearDown()
        {
            Screenshoter.SaveTestFailureScreenshot();
            MyBrowserPool.Release();
        }
    }
}