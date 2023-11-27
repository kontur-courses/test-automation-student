using System;
using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace VacationTests.Infrastructure
{
    public class FirefoxDriverFactory : IWebDriverFactory
    {
        public IWebDriver Create()
        {
            var service = FirefoxDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            var options = new FirefoxOptions();
            var driver = new FirefoxDriver(service, options, TimeSpan.FromSeconds(180));
            driver.Manage().Window.SetSize(1280, 960);
            return driver;
        }
    }
}