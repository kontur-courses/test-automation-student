using System;
using DiExample.Selenium;
using DiExample.Selenium.Page;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace DiExample;

public class Container
{
    private IServiceProvider _provider;
    private IServiceCollection _collection;
    
    public Container()
    {
        _collection = new ServiceCollection();
        _collection.AddTransient<IBrowser, Browser>();
        _collection.AddTransient<IWebDriver, FirefoxDriver>();
        _collection.AddSingleton<IPageFactory, PageFactory>();
    }
    public IServiceProvider BuildServiceProvider()
    {
        _provider = _collection.BuildServiceProvider();
        return _provider;
    }
}