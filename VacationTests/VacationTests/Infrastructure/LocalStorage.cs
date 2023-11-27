using Kontur.Selone.Extensions;
using OpenQA.Selenium;

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
        public long Length => (long)webDriver.JavaScriptExecutor()
            .ExecuteScript("length = window.localStorage.length; return length;");

        // Очистка всего хранилища
        public void Clear()
        {
            webDriver.JavaScriptExecutor().ExecuteScript("window.localStorage.clear();");
        }

        // Получение данных по ключу keyName
        public string GetItem(string keyName)
        {
            return (string)webDriver.JavaScriptExecutor()
                .ExecuteScript($"item = window.localStorage.getItem('{keyName}'); return item;");
        }

        // Получение ключа на заданной позиции
        public string Key(int keyNumber)
        {
            return (string)webDriver.JavaScriptExecutor()
                .ExecuteScript($"key = window.localStorage.key({keyNumber}); return key;");
        }

        // Удаление данных с ключом keyName
        public void RemoveItem(string keyName)
        {
            webDriver.JavaScriptExecutor().ExecuteScript($"window.localStorage.removeItem('{keyName}');");
        }

        // Сохранение пары ключ/значение
        public void SetItem(string keyName, string value)
        {
            webDriver.JavaScriptExecutor().ExecuteScript($"window.localStorage.setItem('{keyName}', '{value}');");
        }
    }
}