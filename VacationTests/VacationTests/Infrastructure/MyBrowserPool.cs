using System;
using System.Collections.Concurrent;
using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using NUnit.Framework;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure
{
    public static class MyBrowserPool
    {
        private static ConcurrentDictionary<string, IWebDriver> webDriversMap = new ConcurrentDictionary<string, IWebDriver>();
        private static string key => TestContext.CurrentContext.Test.ID;
        private static IWebDriverPool pool;

        static MyBrowserPool()
        {
            var browserType = Environment.GetEnvironmentVariable("TEST_BROWSER") ?? "chrome";
            IWebDriverFactory factory;

            switch (browserType)
            {
                case "chrome":
                    factory = new ChromeDriverFactory();
                    break;
                case "firefox":
                    factory = new FirefoxDriverFactory();
                    break;
                default:
                    factory = new ChromeDriverFactory();
                    break;
            }
            
            var cleaner = new DelegateWebDriverCleaner(x => x.ResetWindows());
            pool = new WebDriverPool(factory, cleaner);
        }

        public static IWebDriver Get()
        {
            var browser = webDriversMap.GetOrAdd(key, pool.Acquire());
            return browser;
        }

        public static void Release()
        {
            if (!webDriversMap.TryRemove(key, out var driver))
            {
                throw new Exception($"Не смогли вернуть драйвер в пулл для key={key}");
            }
            pool.Release(driver);
        }

        public static void Dispose()
        {
            pool.Clear();
        }
    }
}