using System;
using Kontur.Selone.Elements;
using Kontur.Selone.Extensions;
using Kontur.Selone.Pages;
using Kontur.Selone.Selectors;
using Kontur.Selone.Selectors.Context;
using Kontur.Selone.Selectors.XPath;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

// О наследовании https://ulearn.me/course/basicprogramming/Nasledovanie_ac2b8cb6-8d63-4b81-9083-eaa77ab0c89c
// Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396
namespace VacationTests.PageObjects
{
    public abstract class PageBase : IPage, IWebElementFinder
    {
        protected PageBase(IContextBy contextBy, ControlFactory controlFactory)
        {
            ControlFactory = controlFactory;
            WrappedDriver = contextBy.SearchContext.WebDriver();
        }

        protected ControlFactory ControlFactory { get; }
        public IWebDriver WrappedDriver { get; }

        public T FindByTid<T>(string tId, SearchArea searchArea = SearchArea.CurrentElement) where T : ControlBase
            => Find<T>(x => x.WithTid(tId), searchArea);

        public T Find<T>(ByLambda byLambda, SearchArea searchArea = SearchArea.CurrentElement) where T : ControlBase
        {
            switch (searchArea)
            {
                case SearchArea.Page:
                case SearchArea.CurrentElement:
                    return ControlFactory.CreateControl<T>(WrappedDriver.Search(byLambda));
                default:
                    throw new Exception($"Не предусмотренная область поиска {searchArea}");
            }
        }

        public ElementsCollection<T> FindCollectionByTid<T>(string tId, SearchArea searchArea = SearchArea.CurrentElement)
            where T : ControlBase
            => FindCollection<T>(x => x.WithTid("tId").FixedByIndex(), searchArea);

        public ElementsCollection<T> FindCollection<T>(ItemByLambda byLambda, SearchArea searchArea = SearchArea.CurrentElement)
            where T : ControlBase
        {
            switch (searchArea)
            {
                case SearchArea.Page:
                case SearchArea.CurrentElement:
                    return ControlFactory.CreateElementsCollection<T>(WrappedDriver, byLambda);
                default:
                    throw new Exception($"Не предусмотренная область поиска {searchArea}");
            }
        }
    }
}