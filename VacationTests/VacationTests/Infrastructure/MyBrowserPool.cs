using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using NUnit.Framework;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure
{
    public class MyBrowserPool
    {
        private static ConcurrentDictionary<string, IWebDriver> webDriversMap = new ConcurrentDictionary<string, IWebDriver>();
        private static string Key => TestContext.CurrentContext.Test.ID ?? "debug";
        private static IWebDriverPool pool;
        public static IWebDriver Get() => webDriversMap.GetOrAdd(Key, _ => pool.Acquire());
        
        public static void Release()
        {
            webDriversMap.TryRemove(Key, out var webDriver);
            pool.Release(webDriver);
        }

        public static void Dispose() => pool.Clear();

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
            pool =  new WebDriverPool(factory, cleaner);
        }
    }
}