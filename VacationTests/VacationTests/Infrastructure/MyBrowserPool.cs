using System.Collections.Concurrent;
using Kontur.Selone.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure
{
    public static class MyBrowserPool
    {
        private static ConcurrentDictionary<string, IWebDriver> pool = new ConcurrentDictionary<string, IWebDriver>();
        
        private static string key => TestContext.CurrentContext.WorkerId ?? "debug";

        public static IWebDriver Get()
        {
            //вторым аргументом GetOrAdd принимает инструкцию как открывать браузер
            var browser = pool.GetOrAdd(key, _ => new ChromeDriverFactory().Create());
            return browser;
        }
        
        public static void Release() => Get().ResetWindows();
        
        public static void Dispose()
        {
            foreach (var browser in pool.Values)
            {
                browser.Dispose();
            }
        }
    }
}