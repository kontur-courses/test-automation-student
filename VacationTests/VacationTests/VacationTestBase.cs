using System.Collections.Concurrent;
using NUnit.Framework;
using OpenQA.Selenium;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageNavigation;

namespace VacationTests
{
    public class VacationTestBase
    {
        // Создание словаря для хранения браузеров пула
        private static readonly ConcurrentDictionary<string, IWebDriver> AcquiredWebDrivers = new();

        // Свойство, которое с помощью NUnit отдаёт текущий WorkerId теста
        private static string TestWorkerId => TestContext.CurrentContext.WorkerId ?? "debug";
        protected IWebDriver WebDriver => GetWebDriver();
        protected ClaimStorage ClaimStorage => new(LocalStorage);
        protected LocalStorage LocalStorage => new(WebDriver);
        private ControlFactory ControlFactory => new(LocalStorage, ClaimStorage);
        protected Navigation Navigation => new(WebDriver, ControlFactory);
        
        // Возвращеия браузера в пул после каждого теста
        [TearDown]
        public void TearDown()
        {
            if (AcquiredWebDrivers.TryRemove(TestWorkerId, out var webDriver))
                AssemblySetUpFixture.WebDriverPool.Release(webDriver);
        }

        // Метод получения текущего WebDriver
        private IWebDriver GetWebDriver()
        {
            return AcquiredWebDrivers.GetOrAdd(TestWorkerId, _ => AssemblySetUpFixture.WebDriverPool.Acquire());
        }
    }
}