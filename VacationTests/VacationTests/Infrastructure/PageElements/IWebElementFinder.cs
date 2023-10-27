using Kontur.Selone.Elements;
using Kontur.Selone.Selectors;

namespace VacationTests.Infrastructure.PageElements
{
    public interface IWebElementFinder
    {
        T Find<T>(ByLambda byLambda, SearchArea searchArea = SearchArea.CurrentElement)
            where T : ControlBase;

        T FindByTid<T>(string tId, SearchArea searchArea = SearchArea.CurrentElement)
            where T : ControlBase;

        ElementsCollection<T> FindCollection<T>(ItemByLambda byLambda, SearchArea searchArea = SearchArea.CurrentElement)
            where T : ControlBase;

        ElementsCollection<T> FindCollectionByTid<T>(string tId, SearchArea searchArea = SearchArea.CurrentElement)
            where T : ControlBase;
    }
}