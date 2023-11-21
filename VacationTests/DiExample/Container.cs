using System;
using DiExample.Selenium;
using DiExample.Selenium.Page;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DiExample
{
    public class Container
    {
        private IServiceProvider provider;
        private IServiceCollection collection;
        
        public Container()
        {
            collection = new ServiceCollection();
            
            collection.AddTransient<IBrowser, Browser>();
            collection.AddTransient<IWebDriver, ChromeDriver>();
            collection.AddSingleton<IPageFactory, PageFactory>();
        }

        public IServiceProvider BuildServiceProvider()
        {
            provider = collection.BuildServiceProvider();
            return provider;
        }
    }
}