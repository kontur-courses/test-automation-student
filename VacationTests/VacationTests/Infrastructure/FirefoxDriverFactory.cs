using System;
using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace VacationTests.Infrastructure
{
    public class FirefoxDriverFactory: IWebDriverFactory
    {
        public IWebDriver Create()
        {
            var chromeDriverService = FirefoxDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            var options = new FirefoxOptions();
            options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
            var chromeDriver = new FirefoxDriver(chromeDriverService, options, TimeSpan.FromSeconds(180));
            chromeDriver.Manage().Window.SetSize(1280, 960);
            return chromeDriver;
        }
    }
}