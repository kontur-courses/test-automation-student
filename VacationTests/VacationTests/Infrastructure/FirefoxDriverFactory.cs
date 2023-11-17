using System;
using Kontur.Selone.WebDrivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace VacationTests.Infrastructure
{
    public class FirefoxDriverFactory : IWebDriverFactory
    {
        public IWebDriver Create()
        {
            var firefoxDriverService = FirefoxDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            var options = new FirefoxOptions();
            //не уверена, что это аналог --no-sandbox, но это самое близкое, что я нагуглила. И оно НЕ работает :(
            options.SetPreference("security.sandbox.content.level", "0");
            //аналогов --disable-extensions я не нашла и как задать размер экрана тоже
            //поэтому ниже просто вроде бы полезнаце штуки
            options.SetPreference("browser.download.dir", "C:\\Windows\\temp");
            options.SetPreference("browser.download.useDownloadDir", true);
            options.SetPreference("browser.download.viewableInternally.enabledTypes", "");
            //options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf;text/plain;application/text;text/xml;application/xml");
            options.SetPreference("pdfjs.disabled", true);  // disable the built-in PDF viewer
            options.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            var firefoxDriver = new FirefoxDriver(firefoxDriverService, options, TimeSpan.FromSeconds(180));
            //firefoxDriver.Manage().Window.SetSize(1280, 560);
            return firefoxDriver;
        }
    }
}