using System;
using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace VacationTests.Infrastructure
{
    // Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396
    public class FirefoxDriverFactory : IWebDriverFactory
    {
        public IWebDriver Create()
        {
            var firefoxService = FirefoxDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            var options = new FirefoxOptions();
            options.AddArguments("--start-maximized", "--disable-extensions", "--width=1280", "--height=960");
            var firefoxDriver = new FirefoxDriver(firefoxService, options, TimeSpan.FromSeconds(180));
            return firefoxDriver;
        }
    }
}