using DiExample.Selenium.Page;
using OpenQA.Selenium;

namespace DiExample.PageObjects.Pages
{
    public class KonturPage : IPage
    {
        private readonly IWebDriver _driver;

        public KonturPage(IWebDriver driver) => _driver = driver;

        public string Url => "https://kontur.ru/";
        public string Title => _driver.Title;
        public string CompositeUrl(string path) => Url + path;

        public IWebElement FirstNew => _driver.FindElement(By.ClassName("tm-article-body"));
    }
}