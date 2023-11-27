using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Concurrent;

namespace VacationTests.Infrastructure
{
    public static class MyBrowserPool
    {
        private static ConcurrentDictionary<string, IWebDriver> webDriversMap;
        private static IWebDriverPool pool;
        private static string key => TestContext.CurrentContext.Test.ID;

        static MyBrowserPool()
        {
            webDriversMap = new ConcurrentDictionary<string, IWebDriver>();
            var cleaner = new DelegateWebDriverCleaner(x => x.ResetWindows());
            pool = new WebDriverPool(new FirefoxDriverFactory(), cleaner);

        }

        public static IWebDriver Get()
        {
            return webDriversMap.GetOrAdd(key, _ => pool.Acquire());
        }

        public static void Dispose()
        {
            pool.Clear();
        }

        public static void Release()
        {
            if (webDriversMap.TryRemove(key, out var browser))
            {
                pool.Release(browser);
            }
            else throw new ArgumentException();
        }
    }
}