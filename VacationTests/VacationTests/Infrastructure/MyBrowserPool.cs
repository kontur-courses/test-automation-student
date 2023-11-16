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
        private static readonly ConcurrentDictionary<string, IWebDriver> webDriversMap = new();

        private static readonly IWebDriverPool pool;

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
                // case "safari":
                //     factory = new SafariDriverFactory();
                //     break;
                default:
                    factory = new ChromeDriverFactory();
                    break;
            }
            
            var delegateWebDriverCleaner = new DelegateWebDriverCleaner(x => x.ResetWindows());
            pool = new WebDriverPool(factory, delegateWebDriverCleaner);
        }

        private static string key => TestContext.CurrentContext.Test.ID ?? "debug";

        public static IWebDriver Get()
        {
            //вторым аргументом GetOrAdd принимает инструкцию как открывать браузер
            var browser = webDriversMap.GetOrAdd(key, _ => pool.Acquire());
            return browser;
        }

        public static void Release()
        {
            var result = webDriversMap.TryRemove(key, out var driver);
            if (result) pool.Release(driver);
        }

        public static void Dispose()
        {
            pool.Clear();
        }
    }
}