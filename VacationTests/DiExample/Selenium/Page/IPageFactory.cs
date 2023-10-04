using System;
using OpenQA.Selenium;

namespace DiExample.Selenium.Page
{
    public interface IPageFactory
    {
        TPage Create<TPage>(IWebDriver webDriver) where TPage : class, IPage;
        TPage Create<TPage>(IWebDriver webDriver, string path) where TPage : class, IPage;
    }

    public class PageFactory : IPageFactory
    {
        #region Реализация

        public TPage Create<TPage>(IWebDriver webDriver) where TPage : class, IPage
        {
            var page = (TPage) Activator.CreateInstance(typeof(TPage), webDriver);
            webDriver.Navigate().GoToUrl(page.Url);
            return page;
        }

        public TPage Create<TPage>(IWebDriver webDriver, string path) where TPage : class, IPage
        {
            var page = (TPage) Activator.CreateInstance(typeof(TPage), webDriver);
            webDriver.Navigate().GoToUrl(page.CompositeUrl(path));
            return page;
        }

        #endregion
    }
}