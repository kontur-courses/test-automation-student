using System;
using Kontur.Selone.Elements;
using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors;
using Kontur.Selone.Selectors.Context;
using Kontur.Selone.Selectors.XPath;
using OpenQA.Selenium;
using VacationTests.Infrastructure.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public abstract class ControlBase : IHaveContainer, IWebElementFinder
    {
        protected ControlFactory ControlFactory { get; }
        public IWebElement Container { get; }

        protected ControlBase(IContextBy contextBy, ControlFactory controlFactory)
        {
            ControlFactory = controlFactory;
            Container = contextBy.SearchContext.SearchElement(contextBy.By);
        }

        public IProp<bool> Present => Container.Present(); // Typo IsPreset. Expression reflection
        public IProp<bool> Visible => Container.Visible();
        public IProp<bool> Disabled => Container.Disabled();

        public void Click()
        {
            Container.Click();
        }

        public override string ToString()
        {
            try
            {
                return $"{Container.TagName} {Container.Text}";
            }
            catch (StaleElementReferenceException)
            {
                return "StaleElement (not found in DOM)";
            }
        }

        public T FindByTid<T>(string tId, SearchArea searchArea = SearchArea.CurrentElement) where T : ControlBase
            => Find<T>(x => x.WithTid(tId), searchArea);

        public T Find<T>(ByLambda byLambda, SearchArea searchArea = SearchArea.CurrentElement) where T : ControlBase
        {
            switch (searchArea)
            {
                case SearchArea.Page:
                    return ControlFactory.CreateControl<T>(Container.Root().Search(byLambda));
                case SearchArea.CurrentElement:
                    return ControlFactory.CreateControl<T>(Container.Search(byLambda));
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
                    return ControlFactory.CreateElementsCollection<T>(Container.Root(), byLambda);
                case SearchArea.CurrentElement:
                    return ControlFactory.CreateElementsCollection<T>(Container, byLambda);
                default:
                    throw new Exception($"Не предусмотренная область поиска {searchArea}");
            }
        }
    }
}