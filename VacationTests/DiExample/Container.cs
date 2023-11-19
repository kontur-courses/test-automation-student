using System;
using DiExample.Selenium;
using DiExample.Selenium.Page;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DiExample;

public class Container
{
    private readonly IServiceCollection _collection;
    private IServiceProvider _provider;

    public Container()
    {
        _collection = new ServiceCollection();

        _collection.AddTransient<IBrowser, Browser>();
        _collection.AddTransient<IWebDriver, ChromeDriver>();
        _collection.AddSingleton<IPageFactory, PageFactory>();
    }

    public IServiceProvider BuildServiceProvider()
    {
        _provider = _collection.BuildServiceProvider();
        return _provider;
    }
}