using DiExample.Selenium.Page;
using DiExample.Selenium;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using NUnit.Framework;
using System.Collections.Concurrent;
using OpenQA.Selenium.Firefox;

namespace DiExample
{
    public static class Container
    {
        static Container()
        {
            ServiceProvider = new ServiceCollection()
               .AddScoped<IPageFactory, PageFactory>()
               .AddScoped<IBrowser, Browser>()
               .AddScoped<IWebDriver, FirefoxDriver>()
               .BuildServiceProvider();
        }

        private static readonly IServiceProvider ServiceProvider;

        // Потокобезопасный словарь для хранения открытых скоупов
        // в качестве ключа может выступать id потока TestContext.CurrentContext.WorkerId
        private static ConcurrentDictionary<string, IServiceScope> scopeMap { get; } = new();
        private static string scopeKey => TestContext.CurrentContext.WorkerId ?? "debug";

        // Берем инстанс объекта из скоупа для текущего потока (теста)
        public static T GetRequiredService<T>() where T : notnull
        {
            var scope = scopeMap.GetOrAdd(scopeKey, _ => ServiceProvider.CreateScope());
            return scope.ServiceProvider.GetRequiredService<T>();
        }

        // Метод для очистки скоупа. После теста мы очищаем данные, и возвращаем браузер в пул
        public static void ScopeDispose()
        {
            if (!scopeMap.TryRemove(scopeKey, out var scope))
            {
                throw new Exception("Не смогли удалить скоуп из scopeMap");
            }

            scope.Dispose();
        }

        public static IServiceProvider BuildServiceProvider()
        {
            return ServiceProvider;
        }

        [TearDown]
        public static void Cleanup()
        {
            ScopeDispose();
        }
    }
}