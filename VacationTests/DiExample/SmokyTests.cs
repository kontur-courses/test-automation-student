using System;
using DiExample.PageObjects.Pages;
using DiExample.Selenium;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DiExample
{
    public class SmokyTests
    {
        private readonly IServiceProvider _serviceProvider = new Container().BuildServiceProvider();
        private IBrowser Browser => _serviceProvider.GetRequiredService<IBrowser>();
        
        [TestCase("Контур"), TestCase("экосистема"), TestCase("бизнеса")]
        public void BrowserShould_BeOpenAndReturn_KonturPage(string substring)
        {
            var page = Browser.GoToPage<KonturPage>();

            Assert.That(page.Title, Contains.Substring(substring));
        }
    }
}