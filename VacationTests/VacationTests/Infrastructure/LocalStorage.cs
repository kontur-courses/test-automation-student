using System;
using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace VacationTests.Infrastructure
{
    public class LocalStorage
    {
        private readonly IWebDriver webDriver;

        public LocalStorage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        // Получение количества элементов в хранилище
        public long Length => (long)webDriver.JavaScriptExecutor().ExecuteScript("return localStorage.length;");

        // Очистка всего хранилища
        public void Clear() => webDriver.JavaScriptExecutor().ExecuteScript("localStorage.clear();");

        // Получение данных по ключу keyName
        public string GetItem(string keyName) => (string)webDriver.JavaScriptExecutor().ExecuteScript($"return localStorage.getItem(\"{keyName}\");");
        
        // Получение ключа на заданной позиции
        public string Key(int keyNumber) => (string)webDriver.JavaScriptExecutor().ExecuteScript($"return localStorage.key({keyNumber});");

        // Удаление данных с ключом keyName
        public void RemoveItem(string keyName) => webDriver.JavaScriptExecutor().ExecuteScript($"localStorage.removeItem(\"{keyName}\");");

        // Сохранение пары ключ/значение
        public void SetItem(string keyName, string value) => webDriver.JavaScriptExecutor().ExecuteScript($"localStorage.setItem(\"{keyName}\", '{value}');");
    }
}